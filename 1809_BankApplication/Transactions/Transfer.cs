namespace _1809_BankApplication {
    class Transfer : Transaction, IHasSender, IHasReceiver {
        public int AccountSenderId { get; set; }
        public int CustomerSenderId { get; set; }
        public int AccountReceiverId { get; set; }
        public int CustomerReceiverId { get; set; }

        public override string InfoAsText => ($"Type of Transaction:\t\tTransfer\n" +
                                              $"Sending Customer ID:\t\t{CustomerSenderId}\n" +
                                              $"Receiving Customer ID:\t\t{CustomerReceiverId}\n" +
                                              $"Sending Account ID:\t\t\t{AccountSenderId}\n" +
                                              $"Receiving Account ID:\t\t{AccountReceiverId}\n" +
                                              $"{AmountString}\n" +
                                              $"{TimeString}");
    }
}
