using AgilePM.Core.Domain;

namespace AgilePM.Identity.Domain.User
{
    public class UserMemento : AbstractMemento
    {
        public UserMemento(string tenantId, string userName, string password,bool isActive,  string firstName, string lastName, string emailAddress, string city, string countryCode, string postalCode, string stateProvince, string streetAddress, string primaryTelephone, string secondaryTelephone)
        {
            TenantId = tenantId;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            City = city;
            CountryCode = countryCode;
            PostalCode = postalCode;
            StateProvince = stateProvince;
            StreetAddress = streetAddress;
            PrimaryTelephone = primaryTelephone;
            SecondaryTelephone = secondaryTelephone;
        }

        public bool IsActive { get; set; }

        public string TenantId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public string City { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
        public string StreetAddress { get; set; }
        public string PrimaryTelephone { get; set; }
        public string SecondaryTelephone { get; set; }
    }
}