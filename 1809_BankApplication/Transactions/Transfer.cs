namespace _1809_BankApplication {
    class Transfer : Transaction, IHasSender, IHasReceiver {
        public int AccountSenderId { get; set; }
        public int CustomerSenderId { get; set; }
        public int AccountReceiverId { get; set; }
        public int CustomerReceiverId { get; set; }

        public string InfoAsText => ($"Sending Customer ID:\t\t{CustomerSenderId}\n" +
                                     $"Receiving Customer ID:\t\t{CustomerReceiverId}\n" +
                                     $"Sending Account ID:\t\t{AccountSenderId}\n" +
                                     $"Receiving Account ID:\t\t{AccountReceiverId}\n" + 
                                     $"{AmountAndTimeString}");
    }
}
