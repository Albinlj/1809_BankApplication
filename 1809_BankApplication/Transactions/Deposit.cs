namespace _1809_BankApplication.Transactions {
    class Deposit : Transaction, IHasReceiver {
        public int AccountReceiverId { get; set; }
        public int CustomerReceiverId { get; set; }

        public override string InfoAsText => ($"Type of Transaction:\t\tDeposit\n" +
                                              $"Receiving Customer ID:\t\t{CustomerReceiverId}\n" +
                                              $"Receiving Account ID:\t\t{AccountReceiverId}\n" +
                                              $"{AmountString}\n" +
                                              $"{TimeString}");
    }
}


