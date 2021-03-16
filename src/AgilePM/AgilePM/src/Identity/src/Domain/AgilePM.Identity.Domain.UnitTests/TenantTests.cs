using System;
using System.Collections.Generic;
using AgilePM.Core.Exceptions;
using AgilePM.Identity.Domain.Contracts;
using AgilePM.Identity.Domain.Contracts.Contracts;
using AgilePM.Identity.Domain.Exceptions;
using AgilePM.Identity.Domain.Tenant;
using Xunit;

namespace AgilePM.Identity.Domain.UnitTests
{
    public class TenantTests
    {
        [Fact]
        public void CreateNewTenant()
        {
            var tenantName = "Microsoft Org";
            TenantId tenantId = new TenantId(Guid.NewGuid().ToString());
            var createNewTenantCommand = new CreateNewTenantCommand(tenantName, true, "description");

            Tenant.Tenant tenant = new Tenant.Tenant(tenantId, createNewTenantCommand);

            Assert.True(tenantId.Equals(tenant.TenantId));
            Assert.Equal(tenantName, tenant.Name);
            Assert.True(tenant.IsActive);
        }

        [Fact]
        public void CreateNewTenant_NameIsEmpty_ExceptionThrown()
        {
            const string Empty_Tenant_Name = "";
            var createNewTenantCommand =TenantContracts.Commands.CreateNewTenant(Empty_Tenant_Name,true, "description");
            
            TenantId tenantId = new TenantId(Guid.NewGuid().ToString());

            var result = Assert.Throws<ParameterNullOrEmptyDomainException>(() => new Tenant.Tenant(tenantId, createNewTenantCommand));

            Assert.Equal("Name" , result.ParameterName);
        }
    }
}
