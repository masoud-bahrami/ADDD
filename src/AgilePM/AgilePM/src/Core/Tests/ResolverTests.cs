using AgilePM.Core.Dispatcher;
using AgilePM.Core.Exceptions;
using AgilePM.Core.Resolver;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AgilePM.Core.UnitTests
{
    public class ResolverTests
    {

        [Fact]
        public void Resolve_UnregisteredDependency_ExceptionThrown()
        {
            IServiceCollection serviceCollection = GetServiceCollection();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IResolver resolver = new ServiceCollectionResolver(serviceProvider);
            var result = Assert.Throws<NotRegisteredDependencyException>(() => resolver.Resolve<FakeCommand>());
            Assert.Equal($"{typeof(FakeCommand)} is not registered", result.Message);
        }

        
        [Fact]
        public void Resolve_DependencyIsRegistered_SuccefullyResovled()
        {
            IServiceCollection serviceCollection = GetServiceCollection();

            serviceCollection.AddScoped<IWantToHandleThisCommand<FakeCommand>, MockFakeCommandHandler>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            var resolver = new ServiceCollectionResolver(serviceProvider);
            var fakeCommandHandler = resolver.Resolve<IWantToHandleThisCommand<FakeCommand>>();
            Assert.NotNull(fakeCommandHandler);
        }

        private IServiceCollection GetServiceCollection() => new ServiceCollection();
    }
}
