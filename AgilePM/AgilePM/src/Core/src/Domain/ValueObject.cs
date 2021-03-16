using System;
using System.Collections.Generic;
using System.Linq;

namespace AgilePM.Core.Domain
{
   public abstract class ValueObject<T>
    {
        public abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            var that = (ValueObject<T>)obj;
            var equalityComponents = that.GetEqualityComponents();

            return this.GetEqualityComponents().SequenceEqual(equalityComponents);
        }
    }
}