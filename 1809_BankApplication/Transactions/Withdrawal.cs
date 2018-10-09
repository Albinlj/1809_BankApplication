using System;

namespace _1809_BankApp {
    public class Withdrawal : Transaction, IHasReceiver {
        public int AccountReceiverId { get; set; }
        public int CustomerReceiverId { get; set; }

        public override string InfoAsText => ($"Type of Transaction:\t\tWithdrawal\n" +
                                              $"Withdrawing Customer ID:\t\t{CustomerReceiverId}\n" +
                                              $"Withdrawing Account ID:\t\t{AccountReceiverId}\n" +
                                              $"{AmountString}\n" +
                                              $"{TimeString}");
    }
}