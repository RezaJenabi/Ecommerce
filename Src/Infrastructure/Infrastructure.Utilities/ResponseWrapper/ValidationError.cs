using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infrastructure.Utilities.ResponseWrapper
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
