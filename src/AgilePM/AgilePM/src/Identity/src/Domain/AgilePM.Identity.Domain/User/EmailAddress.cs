using System;
using AgilePM.Core;
using AgilePM.Core.Domain;

namespace AgilePM.Identity.Domain.User
{
    public class EmailAddress : ValueObject<EmailAddress>
    {
        public EmailAddress(string address)
        {
            this.Address = address;
        }

        public EmailAddress(EmailAddress emailAddress)
            : this(emailAddress.Address)
        {
        }

        protected EmailAddress() { }

        string address;

        public string Address
        {
            get => this.address;
            set
            {
                Check.That().ArgumentNotEmpty("Email",value, "The email address is required.");
                Check.That().ArgumentLength(value, 1, 100, "Email address must be 100 characters or less.");
                Check.That().ArgumentMatches(
                    "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*",
                    value,
                    "Email address format is invalid.");

                this.address = value;
            }
        }

        public override string ToString()
        {
            return "EmailAddress [address=" + Address + "]";
        }

        public override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return address.ToUpper();
        }
    }
}