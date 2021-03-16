using System.Collections.Generic;
using AgilePM.Core.Domain;

namespace AgilePM.Identity.Domain.User
{
    public class ContactInformation : ValueObject<ContactInformation>
    {
        public ContactInformation(EmailAddress emailAddress, PostalAddress postalAddress, Telephone primaryTelephone, Telephone secondaryTelephone)
        {
            EmailAddress = emailAddress;
            PostalAddress = postalAddress;
            PrimaryTelephone = primaryTelephone;
            SecondaryTelephone = secondaryTelephone;
        }

        public EmailAddress EmailAddress { get; private set; }

        public PostalAddress PostalAddress { get; private set; }

        public Telephone PrimaryTelephone { get; private set; }

        public Telephone SecondaryTelephone { get; private set; }


        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return PostalAddress;
            yield return PrimaryTelephone;
            yield return SecondaryTelephone;
        }

        public override string ToString()
        {
            return "ContactInformation [emailAddress=" + EmailAddress
                                                       + ", postalAddress=" + PostalAddress
                                                       + ", primaryTelephone=" + PrimaryTelephone
                                                       + ", secondaryTelephone=" + SecondaryTelephone + "]";
        }
    }
}