using System;
using System.Threading.Tasks;
using AgilePM.Core.Exceptions;
using AgilePM.Core.Resolver;

namespace AgilePM.Core.Dispatcher
{
    public interface IQueryDispatcher
    {
        Task<TResult> RunQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }

    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IResolver _resolver;

        public QueryDispatcher(IResolver resolver)
        {
            _resolver = resolver;
        }

        public async Task<TResult> RunQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            try
            {
                var queryHandler = _resolver.Resolve<IWantToHandleThisQuery<TQuery, TResult>>();

                var result = await queryHandler.RunQuery(query);
                return result;
            }
            catch (NotRegisteredDependencyException e)
            {
                throw new QueryHandlerNotRegisteredException(typeof(TQuery));
            }
            catch (Exception exception)
            {
                //Handle exception
                throw;
            }
        }
    }


    public interface IWantToHandleThisQuery<TQuery, TResult>
        where TQuery : IQuery
    {
        Task<TResult> RunQuery(TQuery query);
    }

    public interface IQuery
    {
    }
}