using System;

namespace AgilePM.Identity.Domain.Exceptions
{
    public class TenantIsNotActiveException : Exception
    {
        public TenantIsNotActiveException(string tenantIdId)
            : base($"Tenant is not Active. Tenant Id is {tenantIdId}")
        {

        }
    }
}