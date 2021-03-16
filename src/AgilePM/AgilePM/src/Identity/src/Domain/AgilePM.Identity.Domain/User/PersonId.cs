using System.Collections.Generic;
using AgilePM.Core.Domain;
using AgilePM.Identity.Domain.Tenant;

namespace AgilePM.Identity.Domain.User
{
    public class PersonId : Identity<PersonId>
    {
        public TenantId TenantId { get; }
        public string UserName { get; }

        public PersonId(TenantId tenantId, string userName)
        {
            TenantId = tenantId;
            UserName = userName;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserName;
            yield return TenantId.Id;
        }
    }
}