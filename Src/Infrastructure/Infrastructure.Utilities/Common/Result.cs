namespace Infrastructure.Utilities.Common
{
    public class Result : IResult
    {
        public string Text { get; set; }
    }

    public interface IResult
    {
    }
}
