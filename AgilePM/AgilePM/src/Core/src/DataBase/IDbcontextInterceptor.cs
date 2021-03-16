using System.Collections.Generic;
using System.Threading.Tasks;
using AgilePM.Core.Domain;

namespace AgilePM.Core.DataBase
{
    public interface IDbContextInterceptor
    {
        Task Start();
        Task Commit();
        Task RoleBack();
        List<Event> Events();
    }
}