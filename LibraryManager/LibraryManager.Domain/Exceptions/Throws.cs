using System.Linq.Expressions;

namespace LibraryManager.Domain.Exceptions
{
    public static class Throws
    {
        public static TArgument ThrowIf<TArgument>(this TArgument argument, Expression<Func<TArgument, bool>> predicate, string message)
        {
            if (predicate.Compile()(argument))
                throw new Exception(message);

            return argument;
        }
    }
}
