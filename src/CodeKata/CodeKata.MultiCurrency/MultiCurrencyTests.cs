using Xunit;

//emergent Design
//Evolutionery Architecture


namespace CodeKata.MultiCurrency
{
    // 5CHF + 5CHF = 10CHF
    
    // equality Dollar and Franc common 
    // equality check object type
    // 5$ + 5$ = 10$

    // 5$ + 10 CHF = 10$ (CHF : Dollar 2:1)
    public class MultiCurrencyTests
    {
        [Fact]
        public void TestDollarMultiplication()
        {
            Money fiveDollar =Money.Dollar(5);

            Money result = fiveDollar.Multiply(multiplier: 2);

            Assert.Equal(5, fiveDollar.Amount);
            Assert.Equal(Money.Dollar(10), result);
        }


        [Fact]
        public void TestFrancMultiplication()
        {
            Money fiveFranc= Money.Franc(amount: 5);
            Assert.Equal(5, fiveFranc.Amount);

            var result = fiveFranc.Multiply(multiplier: 2);

            Assert.Equal(Money.Franc(10), result);
        }

        [Fact]
        public void TestDollarEquality()
        {
            Money fiveDollar =Money.Dollar(5);

            Assert.Equal(Money.Dollar(10), fiveDollar.Multiply(2));
            Assert.Equal(Money.Dollar(15), fiveDollar.Multiply(3));
        }

        [Fact]
        public void TestEquality()
        {
            Assert.NotEqual(Money.Franc(10), Money.Dollar(10));
        }

        [Fact]
        public void TestSimpleAddition()
        {
            // (5$ + 5$ = 10$)
            // (5$ + 5$) * 3 = 30$
            // ((5$ + 5CHF + 2000Rial) + ( 5$ + 5CHF + 2000Rial) * 2 )
            
            //= 20$ if exchange rate 2:1)
            // Reduce

            //var fiveDollar = Money.Dollar(5);
            //Money result = fiveDollar.Plus(Money.Dollar(5));
            //Assert.Equal(Money.Dollar(10), result);

            Money fiveDollar =  Money.Dollar(5);
            
            IMoneyExpression money = fiveDollar.Plus(Money.Dollar(5));
            
            Bank bank = new Bank();
            Money result = bank.Reduce(money, "Dollar");
            Assert.Equal(Money.Dollar(10), result);
        }
    }

    internal interface IMoneyExpression
    {
    }

    class Sum:IMoneyExpression
    {

    }
}


//Red Compile Error -Document the Test case
//Green Make it pass
//Refactor Make it right - remove duplicate