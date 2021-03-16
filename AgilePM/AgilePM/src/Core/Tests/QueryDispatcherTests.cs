using System.Threading.Tasks;
using AgilePM.Core.Dispatcher;
using AgilePM.Core.Resolver;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AgilePM.Core.UnitTests
{
    public class QueryDispatcherTests
    {
        [Fact]
        public void Dispatch_QueryHandleIsNotRegistered_ExceptionThrown()
        {
            ServiceProvider serviceProvider = new ServiceCollection().BuildServiceProvider();
            IResolver resolver = new ServiceCollectionResolver(serviceProvider);
            IQueryDispatcher queryDispatcher = CreateQueryDispatcher(resolver);

            Assert.ThrowsAsync<QueryHandlerNotRegisteredException>(()
                => queryDispatcher.RunQuery<FakeQuery, FakeQueryResult>(new FakeQuery()));
        }

        [Fact]
        public async Task Dispatch_CommandHandlerRegistered_CommandHandlerInvoked()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IWantToHandleThisQuery<FakeQuery, FakeQueryResult>, MockFakeQueryHandler>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IResolver resolver = new ServiceCollectionResolver(serviceProvider);
            IQueryDispatcher queryDispatcher = CreateQueryDispatcher(resolver);

            await queryDispatcher.RunQuery<FakeQuery, FakeQueryResult>(new FakeQuery());

            var queryHandler= resolver.Resolve<IWantToHandleThisQuery<FakeQuery, FakeQueryResult>>();

            ((MockFakeQueryHandler)queryHandler).Verify();
        }

        private IQueryDispatcher CreateQueryDispatcher(IResolver resolver) => new QueryDispatcher(resolver);
    }

    public class MockFakeQueryHandler : IWantToHandleThisQuery<FakeQuery, FakeQueryResult>
    {
        private bool _isCalled;

        public async Task<FakeQueryResult> RunQuery(FakeQuery query)
        {
            _isCalled = true;

            return default(FakeQueryResult);
        }

        internal void Verify()
        {
            Assert.True(_isCalled);
        }

        
    }

    public class FakeQuery : IQuery
    {
    }

    public class FakeQueryResult
    {

    }

}
