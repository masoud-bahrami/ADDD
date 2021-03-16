using System;

namespace AgilePM.Core.Exceptions
{
    public class ParameterNullOrEmptyDomainException : Exception
    {
        public ParameterNullOrEmptyDomainException(string parameterName, object enteredValue , string message)
        :base(message)
        {
            ParameterName = parameterName;
            EnteredValue = enteredValue;
        }

        public string ParameterName { get; }
        public object EnteredValue { get; }
    }
}