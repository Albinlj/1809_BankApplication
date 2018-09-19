using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    struct Transfer : IHasReceiver, IHasSender, ITransaction {
        public int AccountReceiverId { get; set; }
        public int CustomerReceiverId { get; set; }
        public int AccountSenderId { get; set; }
        public int CustomerSenderId { get; set; }

        public decimal Amount { get; set; }
        public DateTime TimeOfTransaction { get; set; }

        public string InfoAsText() => ($"Sending Customer ID:\t\t{CustomerSenderId}" +
                                       $"Receiving Customer ID:\t\t{CustomerReceiverId}" +
                                       $"Sending Account ID:\t\t{AccountSenderId}" +
                                       $"Receiving Account ID:\t\t{AccountReceiverId}" +
                                       $"Amount:\t\t\t{Amount} transferred at {TimeOfTransaction}");
    }
}
