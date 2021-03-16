using System;

namespace AgilePM.Core.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public string ObjectName { get; }

        public ObjectNotFoundException(string objectName, string message)
            : base(message)
        {
            ObjectName = objectName;
        }
    }
}