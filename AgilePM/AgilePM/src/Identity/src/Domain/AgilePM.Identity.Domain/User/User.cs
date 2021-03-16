using AgilePM.Core.Domain;
using AgilePM.Identity.Domain.Contracts;
using AgilePM.Identity.Domain.Contracts.Contracts;
using AgilePM.Identity.Domain.Contracts.Events;
using AgilePM.Identity.Domain.Tenant;
using AgilePM.Identity.Domain.User.DomainService;

namespace AgilePM.Identity.Domain.User
{
    public class User : AggregateRoot<UserId>
    {
        public string Password { get; private set; }
        public string UserName { get; private set; }
        public Person Person { get; private set; }
        public bool IsActive { get; set; }
        private User() : base(null) { }

        public User(UserId identity,bool isActive, RegisterNewUserCommand command, IPasswordDomainService passwordDomainService)
            : base(identity)
        {
            UserName = command.UserName;
            Password = HashPassword(command.Password, passwordDomainService);
            IsActive = isActive;

            Person = new Person(new PersonId(Identity.TenantId, Identity.UserName),
                new FullName(command.PersonInformation.FirstName, command.PersonInformation.LastName)
                , new ContactInformation(new EmailAddress(command.PersonInformation.EmailAddress)
                    , new PostalAddress(command.PersonInformation.AddressStreetAddress
                    , command.PersonInformation.AddressCity, command.PersonInformation.AddressStateProvince
                    , command.PersonInformation.AddressPostalCode
                    , command.PersonInformation.AddressCountryCode),
                    new Telephone(command.PersonInformation.PrimaryTelephone),
                    new Telephone(command.PersonInformation.SecondaryTelephone)));

            Apply(UserContracts.Events.NewUserRegisteredEvent());
        }


        public UserMemento TakeSnapshot()
        {
            var takeSnapshot = new UserMemento(Identity.TenantId.Id
            , UserName
            , Password
            , IsActive
            , Person.FullName.FirstName
            , Person.FullName.LastName
            , Person.ContactInformation.EmailAddress.Address
            , Person.ContactInformation.PostalAddress.City
            , Person.ContactInformation.PostalAddress.CountryCode
            , Person.ContactInformation.PostalAddress.PostalCode
            , Person.ContactInformation.PostalAddress.StateProvince
            , Person.ContactInformation.PostalAddress.StreetAddress
            , Person.ContactInformation.PrimaryTelephone.Number
            , Person.ContactInformation.SecondaryTelephone.Number);

            takeSnapshot.Apply(this.GetQueuedEvents().ToArray());

            return takeSnapshot;
        }

        public static User RestoreFromSnapshot(UserMemento memento)
        {
            return new User
            {
                Identity = new UserId(new TenantId(memento.TenantId), memento.UserName),
                UserName = memento.UserName,
                Password = memento.Password,
                IsActive = memento.IsActive,

                Person = new Person(new PersonId(new TenantId(memento.TenantId), memento.UserName),
                    new FullName(memento.FirstName, memento.LastName)
                    , new ContactInformation(new EmailAddress(memento.EmailAddress)
                        , new PostalAddress(memento.StreetAddress, memento.City, memento.StateProvince, memento.PostalCode, memento.CountryCode)
                        , new Telephone(memento.PrimaryTelephone)
                        , new Telephone(memento.SecondaryTelephone)))
            };
        }

        private string HashPassword(string password, IPasswordDomainService service)
        {
            service.IsWeak(password);
            return service.EncryptedPassword(password);
        }
    }
}