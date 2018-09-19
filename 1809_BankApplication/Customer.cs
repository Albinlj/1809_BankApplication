using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class Customer {
        public int ID { get; set; }
        public long OrgNumber { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }


        public string AsString => $"{ID}: {Name} - {PostalCode}";

        public List<Account> Accounts { get; set; } = new List<Account>();

        public string FullInfoAsString {
            get {
                string returnString = $"Customer ID:\t\t{ID}\n" +
                                      $"Organization Number:\t{OrgNumber}\n" +
                                      $"Name:\t\t\t{Name}\n" +
                                      $"Address:\t\t{Adress}, {PostalCode} {City}\n" +
                                      $"\n" +
                                      $"Accounts:";
                foreach (Account currentAccount in Accounts) {
                    returnString += $"\n{currentAccount.FullInfoAsString}";
                }
                return returnString;
            }
        }

    }
}
