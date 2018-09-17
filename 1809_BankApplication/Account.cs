using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class Account {
        public int ID { get; private set; }
        public int OwnerID { get; private set; }
        public decimal Balance { get; set; }

        public float DebitInterestYearly { get; set; }
        public float DebitInterestDaily { get; private set; }

        public float CreditInterestYearly { get; set; }
        public float CreditInterestDaily { get; private set; }

        public decimal CreditRoof { get; set; }


    }
}
