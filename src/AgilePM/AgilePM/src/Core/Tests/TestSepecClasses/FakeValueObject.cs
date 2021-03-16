using System.Collections.Generic;
using AgilePM.Core.Domain;

namespace AgilePM.Core.UnitTests.TestSepecClasses
{
    public class FakeValueObject : ValueObject<FakeValueObject>
    {
        private readonly string _value;

        public FakeValueObject(string value)
        {
            _value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
}