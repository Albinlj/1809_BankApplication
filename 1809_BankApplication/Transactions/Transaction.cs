using System;

namespace _1809_BankApplication {
    internal abstract class Transaction {
        public decimal Amount { get; set; }
        public DateTime TimeOfTransaction { get; set; }
        public string AmountAndTimeString => $"Amount:\t\t\t{Amount} transferred at {TimeOfTransaction}";
    }
}