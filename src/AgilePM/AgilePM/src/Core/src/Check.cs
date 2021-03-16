using System.Text.RegularExpressions;
using AgilePM.Core.Exceptions;

namespace AgilePM.Core
{
    public class Check
    {
        public static Check That() => new Check();

        public void ArgumentNotEmpty(string name, string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ParameterNullOrEmptyDomainException(name, value, message);
        }

        public void ArgumentNotEmpty(string name, object value, string message)
        {
            if (value == null )
                throw new ParameterNullOrEmptyDomainException(name, value, message);
        }

        public void ArgumentLength(string value, int minimumLength, int maxLength, string message)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < minimumLength || value.Length > maxLength)
                throw ArgumentLengthException.CreateInstance(message);
        }

        public void ArgumentMatches(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(value))
                throw new IllegalArgumentException(message);
        }
    }
}