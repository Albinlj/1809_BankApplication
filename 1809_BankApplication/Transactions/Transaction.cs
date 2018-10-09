using System;

namespace _1809_BankApp {
    public abstract class Transaction {
        public decimal Amount { get; set; }
        public DateTime TimeOfTransaction { get; set; }
        public string AmountString => $"Amount:\t\t\t\t\t\t{Amount:c}";
        public string TimeString => $"Time:\t\t\t\t\t\t{TimeOfTransaction}";
        public virtual string InfoAsText { get; } = "";

        public Transaction() {
            TimeOfTransaction = DateTime.Now;
        }
    }
}