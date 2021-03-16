using System.Threading.Tasks;
using AgilePM.Core.DataBase;
using AgilePM.Core.Dispatcher;
using AgilePM.Core.Resolver;
using AgilePM.Identity.AcceptanceTests.TestSpecificClasses;
using AgilePM.Identity.ApplicationService.Commands.Tenant.CommandHandlers;
using AgilePM.Identity.ApplicationService.Commands.Tenant.Facade;
using AgilePM.Identity.ApplicationService.Query.Tenant;
using AgilePM.Identity.ApplicationService.Query.Tenant.QueryHandlers;
using AgilePM.Identity.ApplicationService.Query.User;
using AgilePM.Identity.ApplicationService.Query.User.QueryHandlers;
using AgilePM.Identity.DataBase;
using AgilePM.Identity.DataBase.DbContext;
using AgilePM.Identity.Domain.Contracts.Contracts;
using AgilePM.Identity.Domain.Services;
using AgilePM.Identity.Domain.Tenant.Repository;
using AgilePM.Identity.Domain.User;
using AgilePM.Identity.Domain.User.DomainService;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AgilePM.Identity.AcceptanceTests
{
    public class UserShould
    {
        //Outside in
        //Driver 
        [Fact]
        public async Task BeCreatedSuccessfully()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IWantToHandleThisCommand<CreateNewTenantCommand>, CreateNewTenantCommandHandler>();
            serviceCollection.AddScoped<IWantToHandleThisCommand<RegisterNewUserCommand>, RegisterNewUserCommandHandler>();
            
            serviceCollection.AddSingleton<IPasswordDomainService, PasswordDomainService>();

            serviceCollection.AddScoped<IdentityDbContext, IdentityDbContextTestSpecific>();
            serviceCollection.AddScoped<IDbContextInterceptor, IdentityDbContextInterceptor>();

            serviceCollection.AddScoped<ITenantRepository, TenantRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            
            serviceCollection.AddScoped<ISemaphore, Semaphore>();

            serviceCollection.AddScoped<IWantToHandleThisQuery<GetTenantByNameQuery, TenantViewModel>, GetTenantQueryHandler>();
            serviceCollection.AddScoped<IWantToHandleThisQuery<GetUserQuery, UserViewModel>, GetUserQueryHandler>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IResolver resolver = new ServiceCollectionResolver(serviceProvider);
            IEventPublisher eventPublisher = new NullEventPublisher();
            ICommandDispatcher commandDispatcher = new CommandDispatcher(resolver, resolver.Resolve<IDbContextInterceptor>(), resolver.Resolve<ISemaphore>() , eventPublisher);

            TenantFacade facade = new TenantFacade(commandDispatcher);

            var microsoftOrg = "Microsoft Org";
            
            await facade.CreateNewTenant(microsoftOrg, true, "description");
            
            IQueryDispatcher queryDispatcher = new QueryDispatcher(resolver);

            TenantViewModel tenant = await queryDispatcher.RunQuery<GetTenantByNameQuery, TenantViewModel>(new GetTenantByNameQuery(microsoftOrg));
            AssertThatTenantIsNotNull(tenant);

            var userName = "BilG";

            PersonInformationDto bilGatesPersonInfo = new PersonInformationDto
            {
                FirstName = "Bil",
                LastName = "Gates",
                EmailAddress = "Bil@Gates.com",
                AddressCity = "Tehran",
                AddressCountryCode = "123",
                AddressPostalCode = "132",
                AddressStateProvince = "Teh",
                AddressStreetAddress = "Teh",
                PrimaryTelephone = "+1-202-555-0155",
                SecondaryTelephone = "+1-202-555-0164"
            };
            await facade.CreateUser(tenant.Id, userName, "123456@123456", bilGatesPersonInfo);

            UserViewModel user = await queryDispatcher.RunQuery<GetUserQuery, UserViewModel>(new GetUserQuery(tenant.Id , userName));

            Assert.True(user.Active);

            Assert.Equal(userName, user.UserName);
        }

        private void AssertThatTenantIsNotNull(TenantViewModel tenantViewModel) => Assert.NotNull(tenantViewModel);
    }
}
