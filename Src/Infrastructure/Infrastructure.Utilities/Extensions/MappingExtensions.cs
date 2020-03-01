using Mapster;

namespace Infrastructure.Utilities.Extensions
{
    public static class MappingExtensions
    {
        public static TTarget MapTo<TTarget>(this object source)
        {
            return source.Adapt<TTarget>();
        }
    }
}
