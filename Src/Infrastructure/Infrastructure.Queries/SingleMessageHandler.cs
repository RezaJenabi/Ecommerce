using Infrastructure.Common;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public abstract class SingleMessageHandler<T, TOut>
        where T : class, IRequest<TOut>
        where TOut : class, IResult, new()
    {
        public abstract Task<TOut> Handler(T message);
    }
}
