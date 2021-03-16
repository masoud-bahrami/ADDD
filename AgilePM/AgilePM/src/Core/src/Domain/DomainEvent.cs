using System;

namespace AgilePM.Core.Domain
{
    public abstract class DomainEvent
    {
        public DateTime OccuredOn { get; }
        protected DomainEvent()
        {
            OccuredOn = DateTime.Now;
        }
    }
}