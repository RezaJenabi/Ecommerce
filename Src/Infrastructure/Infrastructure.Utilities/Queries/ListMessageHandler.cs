using Infrastructure.Utilities.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public abstract class ListMessageHandler<T, TOut>
        where T : class, IRequest<TOut>
        where TOut : class, IResult, new()
    {
        public abstract Task<IList<TOut>> Handler(T message);
    }
}
