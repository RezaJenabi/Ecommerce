using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Utilities.ResponseWrapper
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public IEnumerable<ValidationError> Errors { get; set; }

        public string ReferenceErrorCode { get; set; }
        public string ReferenceDocumentLink { get; set; }

        public ApiException(string message,
            int statusCode = 500,
            IEnumerable<ValidationError> errors = null,
            string errorCode = "",
            string refLink = "") :
            base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
            ReferenceErrorCode = errorCode;
            ReferenceDocumentLink = refLink;
        }

        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
