using System.Collections.Generic;

namespace AgilePM.Core.Domain
{
    public interface IQueueEvent
    {
        void Apply(params Event[] @events);
        List<Event> GetQueuedEvents();
    }
}