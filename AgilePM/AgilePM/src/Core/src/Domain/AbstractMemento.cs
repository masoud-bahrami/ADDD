using System.Collections.Generic;
using System.Linq;

namespace AgilePM.Core.Domain
{
    public abstract class AbstractMemento : IQueueEvent
    {
        private readonly Queue<Event> _eventQueue;

        protected AbstractMemento() => _eventQueue = new Queue<Event>();

        public void Apply(params Event[] @events)
        {
            foreach (var @event in @events) _eventQueue.Enqueue(@event);
        }

        public List<Event> GetQueuedEvents()
        {
            var result = _eventQueue.ToList();
            EmptyQueue();
            return result;
        }
        private void EmptyQueue() => _eventQueue.Clear();
    }
}