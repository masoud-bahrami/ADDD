using System;
using System.Threading.Tasks;
using AgilePM.Core.DataBase;
using AgilePM.Core.Exceptions;
using AgilePM.Core.Resolver;

namespace AgilePM.Core.Dispatcher
{
    public interface ICommandDispatcher
    {
        Task Dispatch<T>(ICommand command) where T : ICommand;
    }

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IResolver _resolver;
        private readonly IDbContextInterceptor _dbContextInterceptor;

        private readonly ISemaphore _semaphore;
        private readonly IEventPublisher _eventPublisher;

        public CommandDispatcher(IResolver resolver, IDbContextInterceptor dbContextInterceptor, ISemaphore semaphore, IEventPublisher eventPublisher)
        {
            _resolver = resolver;
            _dbContextInterceptor = dbContextInterceptor;
            _semaphore = semaphore;
            _eventPublisher = eventPublisher;
        }

        public async Task Dispatch<T>(ICommand command)
            where T : ICommand
        {
            try
            {
                _semaphore.Enter();

                await _dbContextInterceptor.Start();

                var handler = _resolver.Resolve<IWantToHandleThisCommand<T>>();
                await handler.Handle((T)command);

                _semaphore.Exit();

                if (_semaphore.IsThereAnyoneStill() == false)
                {
                    var entities = _dbContextInterceptor.Events();

                    await _dbContextInterceptor.Commit();

                    await _eventPublisher.Queue(entities);
                    // Get Events

                    //EventRepository.Save(events);

                }
            }
            catch (NotRegisteredDependencyException e)
            {
                _semaphore.Exit();
                if (_semaphore.IsThereAnyoneStill() == false)
                    await _dbContextInterceptor.RoleBack();
                throw new CommandHandlerUnregisteredException(command.GetType());

            }
            catch (Exception ex)
            {
                _semaphore.Exit();

                if (_semaphore.IsThereAnyoneStill() == false)
                    await _dbContextInterceptor.RoleBack();
                
                throw;
            }
        }
    }


    public interface ICommand
    {

    }
}