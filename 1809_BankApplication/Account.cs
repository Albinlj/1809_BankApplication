using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApp {
    public class Account {
        private double _debitInterestYearly = .02;
        private double _creditInterestYearly = .1;


        public int Id { get; set; } = 0;
        public int OwnerId { get; set; } = 0;
        public decimal Balance { get; set; } = 0;

        public double DebitInterestYearly {
            get => _debitInterestYearly;
            set => _debitInterestYearly = value > 0 ? value : 0;
        }
        public double CreditInterestYearly {
            get => _creditInterestYearly;
            set => _creditInterestYearly = value > 0 ? value : 0;
        }

        public decimal CreditRoof { get; set; } = 10000;

        public string BalanceString => $"{Balance:C}";
        public string FullInfoAsString => $"{Id,-30}:{BalanceString,20}";





    }
}
