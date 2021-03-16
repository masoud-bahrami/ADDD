using System.Collections.Generic;
using System.Threading.Tasks;
using AgilePM.Core.Dispatcher;
using AgilePM.Core.Domain;

namespace AgilePM.Identity.AcceptanceTests.TestSpecificClasses
{
    public class NullEventPublisher : IEventPublisher
    {
        public Task Queue(List<Event> entities) => Task.CompletedTask;
    }
}