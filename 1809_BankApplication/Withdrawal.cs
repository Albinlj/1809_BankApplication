using System;

namespace _1809_BankApplication {
    struct Withdrawal : IHasSender, ITransaction {
        public int AccountSenderId { get; set; }
        public int CustomerSenderId { get; set; }

        public decimal Amount { get; set; }
        public DateTime TimeOfTransaction { get; set; }

        public string InfoAsText() => ($"Sending Customer ID:\t\t{CustomerSenderId}" +
                                       $"Sending Account ID:\t\t{AccountSenderId}" +
                                       $"Amount:\t\t\t{Amount} transferred at {TimeOfTransaction}");
    }
}