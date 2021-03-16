using System;
using System.Runtime.CompilerServices;
using AgilePM.Core;
using AgilePM.Core.Domain;
using AgilePM.Core.Exceptions;
using AgilePM.Identity.Domain.Contracts.Contracts;
using AgilePM.Identity.Domain.Exceptions;
using AgilePM.Identity.Domain.User;
using AgilePM.Identity.Domain.User.DomainService;

[assembly: InternalsVisibleTo("AgilePM.Identity.DataBase")]
namespace AgilePM.Identity.Domain.Tenant
{
    public class Tenant : AggregateRoot<TenantId>
    {
        public TenantId TenantId { get; set; }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                AssertTenantNameIsValid(value);
                _name = value;
            }
        }

        public bool IsActive { get; }
        public string Description { get; private set; }

        internal string Id
        {
            get => TenantId.Id;
            set => TenantId = new TenantId(value);
        }

        private Tenant() : base(null) { }

        public Tenant(TenantId tenantId, CreateNewTenantCommand command)
                : base(tenantId)
        {
            // Exception and Information

            // Design by Contracts > Berterand Meyer(CQS)
            // Object Oriented Construction
            // Eiffel

            // Business Rules
            // Invariants
            // Pre Conditions >> client 
            // Post Conditions >> Aggregate >> Aggregate Root

            // Always Valid

            // Command --> Aggregate Root --> <Events>

            // Aggregate Root act as a State Machine
            // Command (plus other parameters) --> List of Events
            // Initiate 

            // Defensive Programming

            AssertTenantIdIsValid(tenantId);
            AssertTenantNameIsValid(command.TenantName);
            AssertDescriptionIsValid(command.Description);

            TenantId = tenantId;
            Name = command.TenantName;
            Description = command.Description;
            IsActive = command.TenantIsActive;

            //RegisterDomainEvent(new NewTenantCreated(Identity.Id));
        }

        private Tenant(TenantMemento memento) : base(new TenantId(memento.TenantId))
        {

        }

        private void AssertDescriptionIsValid(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new Exception();
        }

        private void AssertTenantNameIsValid(string tenantName)
        {
            Check.That().ArgumentNotEmpty("TenantName", tenantName, "Tenant can not be null or empty");

            if (tenantName.Length <= 3)
                throw new ParameterLengthDomainException(nameof(Name), tenantName, 3);
        }

        private void AssertTenantIdIsValid(TenantId tenantId) => Check.That().ArgumentNotEmpty("TenantId",tenantId , "Tenant can be empty");


        public TenantMemento GetMemento()
        {
            var tenantMemento = new TenantMemento(TenantId.Id, Name, IsActive, Description);
            return tenantMemento;
        }

        public static Tenant RestoreFromMemento(TenantMemento memento)
        {
            return new Tenant(memento);
        }


        //Factory method
        public User.User RegisterNewUser(RegisterNewUserCommand command, IPasswordDomainService passwordDomainService)
        {
            AssertThatTenantIsActive();

            return new User.User(new UserId(TenantId, command.UserName),true, command , passwordDomainService);
        }

        private void AssertThatTenantIsActive()
        {
            if (IsActive == false)
                throw new TenantIsNotActiveException(TenantId.Id);
        }
    }

    public class TenantMemento
    {
        public string TenantId { get; }
        public string Name { get; }
        public bool IsActive { get; }
        public string Description { get; }

        public TenantMemento(string tenantId, string name, bool isActive, string description)
        {
            TenantId = tenantId;
            Name = name;
            IsActive = isActive;
            Description = description;
        }
    }
}