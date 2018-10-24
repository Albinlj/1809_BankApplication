namespace _1809_BankApp.Transactions
{
    public class InterestApplication : Transaction, IHasReceiver
    {
        public int AccountReceiverId { get; set; }
        public int CustomerReceiverId { get; set; }
        public double InterestRate { get; set; }

        public override string InfoAsText => ($"Type of Transaction:\t\tInterest\n" +
                                              $"Receiving Customer ID:\t\t{CustomerReceiverId}\n" +
                                              $"Receiving Account ID:\t\t{AccountReceiverId}\n" +
                                              $"Interest Rate Used:\t\t{InterestRate}\n" + 
                                              $"{AmountString}\n" +
                                              $"{TimeString}");
    }
}


