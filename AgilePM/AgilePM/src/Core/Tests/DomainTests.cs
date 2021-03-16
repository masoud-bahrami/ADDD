using System.Collections.Generic;
using AgilePM.Core.Domain;
using AgilePM.Core.UnitTests.TestSepecClasses;
using Xunit;

namespace AgilePM.Core.UnitTests
{
    public class DomainTests
    {
        [Fact]
        public void TestValueObjectEquality()
        {
            ValueObject<FakeValueObject> fakeValueObject1 = new FakeValueObject("Value");
            ValueObject<FakeValueObject> fakeValueObject2 = new FakeValueObject("Value");

            Assert.Equal(fakeValueObject1, fakeValueObject2);
        }

        [Fact]
        public void TestValueObjectNotEquality()
        {
            ValueObject<FakeValueObject> fakeValueObject1 = new FakeValueObject("Value1");
            ValueObject<FakeValueObject> fakeValueObject2 = new FakeValueObject("Value2");

            Assert.NotEqual(fakeValueObject1, fakeValueObject2);
        }


        [Fact]
        public void TestEntityEquality()
        {
            FakeEntityIdentity identity = new FakeEntityIdentity(1);

            Entity<FakeEntityIdentity> fakeEntity1 = new FakeEntity(identity, "value1");
            Entity<FakeEntityIdentity> fakeEntity2 = new FakeEntity(identity, "value2");

            Assert.Equal(fakeEntity1, fakeEntity2);
        }
    }

    public class FakeEntity : Entity<FakeEntityIdentity>
    {
        public string Value { get; }

        public FakeEntity(FakeEntityIdentity identity, string value)
            : base(identity)
        {
            Identity = identity;
            Value = value;
        }
    }

    public class FakeEntityIdentity : Identity<FakeEntityIdentity>
    {
        private readonly long _id;

        public FakeEntityIdentity(long id)
        {
            _id = id;
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return _id;
        }
    }
}