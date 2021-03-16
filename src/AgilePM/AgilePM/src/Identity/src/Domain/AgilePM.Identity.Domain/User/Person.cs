using AgilePM.Core.Domain;

namespace AgilePM.Identity.Domain.User
{
    public class Person : Entity<PersonId>
    {
        public FullName FullName { get; private set; }
        public ContactInformation ContactInformation { get; private set; }

        public Person(PersonId identity, FullName fullName, ContactInformation contactInformation) : base(identity)
        {
            FullName = fullName;
            ContactInformation = contactInformation;
        }
    }
}