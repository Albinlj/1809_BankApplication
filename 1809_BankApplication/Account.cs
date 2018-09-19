using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class Account {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public decimal Balance { get; set; }

        public AccountManager myManager { get; set; }

        public float DebitInterestYearly { get; set; }
        public float DebitInterestDaily { get; private set; }
        public float CreditInterestYearly { get; set; }
        public float CreditInterestDaily { get; private set; }
        public decimal CreditRoof { get; set; }


        public string StringBalance => $"{Balance:C}";
        public string FullInfoAsString => $"{Id}:\t\t\t{StringBalance}";

        public static implicit operator int(Account account) {
            return account.Id;
        }

        //        public static implicit operator Account(int inputInt) {
        //return this.myManager.GetAccountByAccountId()
        //        }

        public Account(AccountManager manager) {
            myManager = manager;
        }

    }
}
