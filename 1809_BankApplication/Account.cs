using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class Account {
        private double _debitInterestYearly = 1.02;
        private double _creditInterestYearly = 1.1;

        public AccountManager MyManager { get; private set; }

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public decimal Balance { get; set; }

        public double DebitInterestYearly {
            get => _debitInterestYearly;
            set => _debitInterestYearly = value > 0 ? value : 0;
        }
        public double CreditInterestYearly {
            get => _creditInterestYearly;
            set => _creditInterestYearly = value > 0 ? value : 0;
        }

        public decimal CreditRoof { get; set; } = 10000;

        public string BalanceString => $"{Balance: C}";
        public string FullInfoAsString => $"{Id,-30}:{BalanceString,20}";

        public static implicit operator int(Account account) {
            return account.Id;
        }

        public Account(AccountManager manager) {
            MyManager = manager;
        }

    }
}
