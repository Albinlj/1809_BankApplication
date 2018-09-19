using System;

namespace _1809_BankApplication {
    struct Deposit : IHasReceiver, ITransaction {
        public int AccountReceiverId { get; set; }
        public int CustomerReceiverId { get; set; }

        public decimal Amount { get; set; }
        public DateTime TimeOfTransaction { get; set; }

        public string InfoAsText() => ($"Receiving Customer ID:\t\t{CustomerReceiverId}" +
                                       $"Receiving Account ID:\t\t{AccountReceiverId}" +
                                       $"Amount:\t\t\t{Amount} transferred at {TimeOfTransaction}");
    }
}