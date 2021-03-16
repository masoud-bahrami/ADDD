using AgilePM.Core.Domain;

namespace AgilePM.Identity.Domain.User
{
    public class PostalAddress : ValueObject<PostalAddress>
    {

        public string City { get; private set; }
        public string CountryCode { get; private set; }
        public string PostalCode { get; private set; }
        public string StateProvince { get; private set; }
        public string StreetAddress { get; private set; }

        public PostalAddress(
            string streetAddress,
            string city,
            string stateProvince,
            string postalCode,
            string countryCode)
        {
            City = city;
            CountryCode = countryCode;
            PostalCode = postalCode;
            StateProvince = stateProvince;
            StreetAddress = streetAddress;
        }


        public override string ToString()
        {
            return "PostalAddress [streetAddress=" + StreetAddress
                                                   + ", city=" + City + ", stateProvince=" + StateProvince
                                                   + ", postalCode=" + PostalCode
                                                   + ", countryCode=" + CountryCode + "]";
        }

        public override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return StreetAddress;
            yield return City;
            yield return StateProvince;
            yield return PostalCode;
            yield return CountryCode;
        }
    }
}