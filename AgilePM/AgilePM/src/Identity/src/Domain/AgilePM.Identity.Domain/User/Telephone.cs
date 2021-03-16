using System.Collections.Generic;
using AgilePM.Core;
using AgilePM.Core.Domain;

namespace AgilePM.Identity.Domain.User
{

    public class Telephone : ValueObject<Telephone>
    {
        public Telephone(string number)
        {
            Number = number;
        }

        private string _number;

        public string Number
        {
            get => _number;
            set
            {
                Check.That().ArgumentNotEmpty("Number", value, "Telephone number is required.");
                Check.That().ArgumentLength(value, 5, 20, "Telephone number may not be more than 20 characters.");
                Check.That().ArgumentMatches(
                    "((\\(\\d{3}\\))|(\\d{3}-))\\d{3}-\\d{4}",
                    value,
                    "Telephone number or its format is invalid.");

                _number = value;
            }
        }

        public override string ToString() => "Telephone [number=" + Number + "]";

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}