namespace AgilePM.Core.Domain
{
    public abstract class Entity<TIdentity>
        where TIdentity : Identity<TIdentity>
    {
        protected TIdentity Identity;

        protected Entity(TIdentity identity) => Identity = identity;

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;

            var that = (Entity<TIdentity>)obj;
            return this.Identity == that.Identity;
        }
    }
}