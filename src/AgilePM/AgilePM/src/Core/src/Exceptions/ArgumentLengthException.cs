using System;

namespace AgilePM.Core.Exceptions
{
    public class ArgumentLengthException : Exception
    {
        public static ArgumentLengthException CreateInstance(string message)
        {
            return new ArgumentLengthException(message);
        }

        private ArgumentLengthException(string message)
            :base(message){
            
        }
    }
}