using System.Collections.Generic;

namespace Infrastructure.Utilities.Common
{
    public class Result : IResult
    {
        public int StatusCode { get; set; } = 200;
        public bool IsSuccessed { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public IList<string> Errors { get; set; } = new List<string>();
    }

    public class ListMessageResult<T> : Result
    {
        public List<T> Entities { get; set; }
    }

    public class SingleMessageResult<T> : Result
    {
        public T Entity { get; set; }
    }

    public interface IResult
    {
        bool IsSuccessed { get; set; }
        string Message { get; set; }
        IList<string> Errors { get; set; }
    }
}
