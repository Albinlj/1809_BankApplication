namespace _1809_BankApplication.Transactions {
    class Deposit : Transaction, IHasReceiver {
        public int AccountReceiverId { get; set; }
        public int CustomerReceiverId { get; set; }

        public string InfoAsText => ($"Receiving Customer ID:\t\t{CustomerReceiverId}\n" +
                                     $"Receiving Account ID:\t\t{AccountReceiverId}\n" +
                                     $"{AmountAndTimeString}");
    }
}


