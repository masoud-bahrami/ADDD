using System.Collections.Generic;
using System.Threading.Tasks;
using AgilePM.Core.Domain;

namespace AgilePM.Core.Dispatcher
{
    public interface IEventPublisher
    {
        Task Queue(List<Event> entities);
    }
}