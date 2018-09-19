using System;

namespace _1809_BankApplication
{
    internal interface ITransaction
    {
        decimal Amount { get; set; }
        DateTime TimeOfTransaction { get; set; }
        string InfoAsText();
    }
}