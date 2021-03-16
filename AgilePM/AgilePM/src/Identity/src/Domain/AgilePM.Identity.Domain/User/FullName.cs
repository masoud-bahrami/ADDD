using System.Collections.Generic;
using AgilePM.Core;
using AgilePM.Core.Domain;

namespace AgilePM.Identity.Domain.User
{
    public class FullName : ValueObject<FullName>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        
        public FullName(string firstName, string lastName)
        {
            AssertThatFirstNameIsValid(firstName);
            AssertThatLastNameIsValid(lastName);

            FirstName = firstName;
            LastName = lastName;
        }

        //Imutable
        public FullName ChangeFirstName(string newFirstName) => new FullName(newFirstName, LastName);
        public FullName ChangeLastName(string newLastName) => new FullName(FirstName, newLastName);
        
        public string FormattedName() => $"{FirstName} - {LastName}";

        private void AssertThatLastNameIsValid(string lastName)
            => Check.That().ArgumentNotEmpty("LastName", lastName, "Last name can not be empty");

        private void AssertThatFirstNameIsValid(string firstName)
            => Check.That().ArgumentNotEmpty("FirstName", firstName, "First name can not be empty");

        public override string ToString() => $"FullName [firstName={FirstName}, lastName={LastName}]";


        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}