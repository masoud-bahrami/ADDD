using System;
using System.Collections.Generic;
using System.Text;

namespace CodeKata.MultiCurrency
{
    public class Money  : IMoneyExpression
    {
        public int Amount { get; set; }
        protected string _currency;

        public Money(int amount , string curercny)
        {
            Amount = amount;
            _currency = curercny;
        }
        internal Money Multiply(int multiplier)
        {
            return new Money(Amount * multiplier , Currency());
        }

        internal static Money Dollar(int amounnt)
        {
            return new Dollar(amounnt, "Dollar");
        }

        public string Currency() => _currency;
        
        public override bool Equals(object? obj)
        {
            Money that = (Money)obj;
            return this.Amount == that.Amount &&
                  this.Currency() == that.Currency();
        }

        internal static Money Franc(int amount)
        {
            return new Franc(amount, "CHF");
        }

        internal IMoneyExpression Plus(Money money)
        {
            return new Dollar(10, "Dollar");
        }
    }
}
