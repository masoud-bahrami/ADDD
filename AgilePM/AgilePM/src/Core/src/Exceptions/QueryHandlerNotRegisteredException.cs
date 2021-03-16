using System;

namespace AgilePM.Core.Exceptions
{
    public class QueryHandlerNotRegisteredException : Exception
    {
        public Type QueryHandlerType { get; }

        public QueryHandlerNotRegisteredException(Type queryHandlerType)
        {
            QueryHandlerType = queryHandlerType;
        }
    }
}