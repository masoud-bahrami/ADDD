using System;

namespace AgilePM.Core.Exceptions
{
    
    public class NotRegisteredDependencyException : Exception
    {
        public NotRegisteredDependencyException(Type componentType)
            : base($"{componentType} is not registered")
        {

        }
    }
}