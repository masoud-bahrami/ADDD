using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgilePM.Core.DataBase;
using AgilePM.Core.Dispatcher;
using AgilePM.Core.Domain;
using AgilePM.Core.Exceptions;
using AgilePM.Core.Resolver;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AgilePM.Core.UnitTests
{
    public class CommandDispatcherTests
    {
        [Fact]
        public void Dispatch_CommandHandlerUnRegistered_ExceptionThrown()
        {
            ServiceProvider serviceProvider = new ServiceCollection().BuildServiceProvider();
            IResolver resolver = new ServiceCollectionResolver(serviceProvider);
            ICommandDispatcher commandDispatcher = CreateCommandDispatcher(resolver);

            var result = Assert.ThrowsAsync<CommandHandlerUnregisteredException>(() => commandDispatcher.Dispatch<FakeCommand>(new FakeCommand()));

            Assert.Equal(typeof(CommandHandlerUnregisteredException), result.Result.GetType());
        }


        [Fact]
        public void Dispatch_CommandHandlerRegistered_CommandHandlerInvoked()
        {
            //Interaction based test
            //Mock
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IWantToHandleThisCommand<FakeCommand>, MockFakeCommandHandler>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IResolver resolver = new ServiceCollectionResolver(serviceProvider);

            ICommandDispatcher commandDispatcher = CreateCommandDispatcher(resolver);

            commandDispatcher.Dispatch<FakeCommand>(new FakeCommand());

            var fakeCommandHandler = resolver.Resolve<IWantToHandleThisCommand<FakeCommand>>();

            ((MockFakeCommandHandler)fakeCommandHandler).Verify();
        }

        private ICommandDispatcher CreateCommandDispatcher(IResolver resolver)
            => new CommandDispatcher(resolver, NullDbContextInterceptor.New(), SemaphoreNull.New()
            ,NullEventPublisher.New());


    }

    internal class NullEventPublisher : IEventPublisher
    {
        public static IEventPublisher New() => new NullEventPublisher();

        public Task Queue(List<Event> entities) => Task.CompletedTask;
    }

    internal class SemaphoreNull : ISemaphore
    {
        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public bool IsThereAnyoneStill() => false;

        public static ISemaphore New()
        {
            return new SemaphoreNull();
        }
    }

    internal class NullDbContextInterceptor : IDbContextInterceptor
    {
        public Task Start() => Task.CompletedTask;

        public Task Commit() => Task.CompletedTask;

        public Task RoleBack() => Task.CompletedTask;
        public List<Event> Events()
        {
            throw new NotImplementedException();
        }

        public static IDbContextInterceptor New()
        {
            return new NullDbContextInterceptor();
        }
    }


    public class FakeCommand : ICommand
    {
    }


    public class MockFakeCommandHandler : IWantToHandleThisCommand<FakeCommand>
    {
        private bool _isInvoked;

        public async Task Handle(FakeCommand command)
        {
            _isInvoked = true;
        }

        internal void Verify()
        {
            Assert.True(_isInvoked);
        }
    }

}
