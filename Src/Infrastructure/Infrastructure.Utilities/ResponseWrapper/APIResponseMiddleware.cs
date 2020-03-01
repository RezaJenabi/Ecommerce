using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using EnumsNET;
using Infrastructure.Utilities.Enums;
using Infrastructure.Utilities.Extensions;
using Infrastructure.Utilities.FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Utilities.ResponseWrapper
{
    public class APIResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public APIResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsSwagger(context))
                await this._next(context);
            else
            {
                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    try
                    {
                        await _next.Invoke(context);

                        switch (context.Response.StatusCode)
                        {
                            case (int)CustomHttpStatusCode.OK:
                                {
                                    var body = await FormatResponse(context.Response);
                                    await HandleSuccessRequestAsync(context, body, context.Response.StatusCode);
                                    break;
                                }
                            case (int)CustomHttpStatusCode.FluentValidation:
                                {
                                    var body = await FormatResponse(context.Response);
                                    await HandleBadRequestValidationAsync(context, body);
                                    break;
                                }
                            default:
                                {
                                    await HandleNotSuccessRequestAsync(context, context.Response.StatusCode);
                                    break;
                                }
                        }
                    }
                    catch (Exception ex)
                    {
                        await HandleExceptionAsync(context, ex);
                    }
                    finally
                    {
                        responseBody.Seek(0, SeekOrigin.Begin);
                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ApiError apiError = null;
            APIResponse apiResponse = null;
            int code = 0;

            if (exception is ApiException)
            {
                var ex = exception as ApiException;
                apiError = new ApiError(ex.Message);
                apiError.ValidationErrors = ex.Errors;
                apiError.ReferenceErrorCode = ex.ReferenceErrorCode;
                apiError.ReferenceDocumentLink = ex.ReferenceDocumentLink;
                code = ex.StatusCode;
                context.Response.StatusCode = code;

            }
            else if (exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                code = (int)CustomHttpStatusCode.Unauthorized;
                context.Response.StatusCode = code;
            }
            else
            {
#if !DEBUG
            var msg = "An unhandled error occurred.";
            string stack = null;
#else
                var msg = exception.GetBaseException().Message;
                string stack = exception.StackTrace;
#endif

                apiError = new ApiError(msg);
                apiError.Details = stack;
                code = (int)CustomHttpStatusCode.InternalServerError;
                context.Response.StatusCode = code;
            }

            context.Response.ContentType = "application/json";
            //EnumDescription((ResponseMessageEnum)ResponseMessageEnum.Exception);
            apiResponse = new APIResponse(code, ResponseMessageEnum.Exception.AsString(EnumFormat.Description), null, apiError);

            var json = apiResponse.SerializeObject();

            return context.Response.WriteAsync(json);
        }

        private static Task HandleNotSuccessRequestAsync(HttpContext context, int code)
        {
            context.Response.ContentType = "application/json";

            ApiError apiError = null;
            APIResponse apiResponse = null;

            if (code == (int)CustomHttpStatusCode.NotFound)
                apiError = new ApiError("The specified URI does not exist. Please verify and try again.");
            else if (code == (int)CustomHttpStatusCode.NoContent)
                apiError = new ApiError("The specified URI does not contain any content.");
            else
                apiError = new ApiError("Your request cannot be processed. Please contact a support.");

            apiResponse = new APIResponse(code, ResponseMessageEnum.Failure.AsString(EnumFormat.Description), null, apiError);
            context.Response.StatusCode = code;

            var json = apiResponse.SerializeObject();
            context.Response.Body.Position = 0;
            return context.Response.WriteAsync(json);
        }

        private static Task HandleBadRequestValidationAsync(HttpContext context, string body, int code = 710)
        {
            context.Response.ContentType = "application/json";

            ApiError apiError = null;
            APIResponse apiResponse = null;
            string errorMessage = "";

            body.DeserializeObject<ErrorFluentValidation>().Errors.ForEach(x => errorMessage += $"{x},");
            apiError = new ApiError(errorMessage.Remove(errorMessage.Length - 1));
            apiResponse = new APIResponse(code, ResponseMessageEnum.Failure.AsString(EnumFormat.Description), null, apiError);
            context.Response.StatusCode = code;

            var json = apiResponse.SerializeObject();

            return context.Response.WriteAsync(json);
        }

        private static Task HandleSuccessRequestAsync(HttpContext context, object body, int code)
        {
            context.Response.ContentType = "application/json";
            string jsonString, bodyText = string.Empty;
            APIResponse apiResponse = null;


            if (!body.ToString().IsValidJson())
                bodyText = body.SerializeObject();
            else
                bodyText = body.ToString();

            dynamic bodyContent = bodyText.DeserializeObject<dynamic>();
            Type type;

            type = bodyContent?.GetType();

            if (type == typeof(JObject))
            {
                apiResponse = new APIResponse(code, ResponseMessageEnum.Success.AsString(EnumFormat.Description), bodyContent, null);
                jsonString = apiResponse.SerializeObject();
            }
            else
            {
                apiResponse = new APIResponse(code, ResponseMessageEnum.Success.AsString(EnumFormat.Description), bodyContent, null);
                jsonString = apiResponse.SerializeObject();
            }

            return context.Response.WriteAsync(jsonString);
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var plainBodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return plainBodyText;
        }

        private bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");
        }
    }
}
