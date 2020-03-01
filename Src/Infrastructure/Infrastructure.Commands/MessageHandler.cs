using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.Commands
{
    public abstract class MessageHandler<T, TOut>
        where T : class, IRequest<TOut>
        where TOut : class, IResult, new()
    {
        public abstract Task<TOut> Handler(T message);
    }
}
