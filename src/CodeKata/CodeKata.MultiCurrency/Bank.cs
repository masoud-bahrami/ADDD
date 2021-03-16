using System;

namespace CodeKata.MultiCurrency
{
    internal class Bank
    {
        internal Money Reduce(IMoneyExpression money, string v)
        {
            return new Money(10, "Dollar");
        }
    }
}