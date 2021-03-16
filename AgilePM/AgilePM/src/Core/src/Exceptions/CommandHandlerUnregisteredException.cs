using System;

namespace AgilePM.Core.Exceptions
{
    public class CommandHandlerUnregisteredException : Exception
    {
        public Type CommandType { get; }

        public CommandHandlerUnregisteredException(Type commandType)
        {
            CommandType = commandType;
        } 
    }
}