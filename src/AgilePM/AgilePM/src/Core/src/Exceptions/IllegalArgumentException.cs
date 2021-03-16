using System;

namespace AgilePM.Core.Exceptions
{
    public class IllegalArgumentException : Exception
    {
        public IllegalArgumentException(string message)
            :base(message){
            
        }
    }
}