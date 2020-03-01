namespace Infrastructure.Common
{
    public class Result : IResult
    {
        public string Text { get; set; }
    }

    public interface IResult
    {
    }
}
