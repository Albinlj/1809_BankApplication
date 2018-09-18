using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class Transaction {
        public int AccountReceiverId { get; set; }
        public int AccountSenderId { get; set; }
        public int CustomerReceiverId { get; set; }
        public int CustomerSenderId { get; set; }


        public decimal Amount { get; set; }
        public DateTime TimeOfTransaction { get; set; }


        public Transaction() {
            TimeOfTransaction = DateTime.Now;

        }

        public string InfoAsText() {
            string returnString = "";
            Console.WriteLine($"Sending Customer ID:\t\t{AccountSenderId}" +
                $"Receiving Customer ID:\t\t{CustomerReceiverId}" +
                $"Sending Account ID:\t\t{AccountSenderId}" +
                $"Receiving Account ID:\t\t{AccountReceiverId}" +
                $"Amount:\t\t\t{Amount} transferred at {TimeOfTransaction}");
            return returnString;
        }
    }
}
