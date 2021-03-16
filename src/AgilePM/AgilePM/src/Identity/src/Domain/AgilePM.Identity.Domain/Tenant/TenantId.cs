using AgilePM.Core.Domain;
using System.Collections.Generic;

namespace AgilePM.Identity.Domain.Tenant
{
    public class TenantId : Identity<TenantId>
    {
        private TenantId() { }
        public string Id { get; }

        public TenantId(string id)
        {
            Id = id;
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}