using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilePM.Core.DataBase;
using AgilePM.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AgilePM.Identity.DataBase.DbContext
{
    public class IdentityDbContextInterceptor : IDbContextInterceptor
    {
        private readonly IdentityDbContext _dbContext;

        public IdentityDbContextInterceptor(IdentityDbContext dbContext) => _dbContext = dbContext;

        public Task Start() => Task.CompletedTask;

        public async Task Commit() => await _dbContext.SaveChangesAsync();

        public Task RoleBack()
        {
            foreach (var entityEntry in _dbContext.ChangeTracker.Entries())
                _dbContext.Entry(entityEntry.Entity).State = EntityState.Detached;

            return Task.CompletedTask;
        }

        public List<Event> Events()
        {
            List<Event> result = new List<Event>();

            var queueEvents = _dbContext.ChangeTracker.Entries()
                .Select(en => en.Entity)
                .Select(a => a as IQueueEvent)
                .ToList();

            //CreateNewUser
            //Handler >> Aggregate Root names User >> Apply
            // Repository >> Memento >> Events

            foreach (var queueEvent in queueEvents)
            {
                var events = queueEvent.GetQueuedEvents();
                result.AddRange(events);
            }

            return result;
        }
    }
}