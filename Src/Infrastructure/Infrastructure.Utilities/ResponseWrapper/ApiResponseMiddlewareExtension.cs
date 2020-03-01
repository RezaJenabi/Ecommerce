using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Utilities.ResponseWrapper
{
    public static class ApiResponseMiddlewareExtension
    {
        public static IApplicationBuilder UseAPIResponseWrapperMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIResponseMiddleware>();
        }
    }
}
