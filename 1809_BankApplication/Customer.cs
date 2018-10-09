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

        public List<Account> MyAccounts { get; set; } = new List<Account>();

        public string FullInfoAsString {
            get {
                const int spacing1 = -30;
                const string divider = "-    ";
                string returnString = $"{"Customer ID:", spacing1}{divider}{ID}\n" +
                                      $"{"Organization Number", spacing1}{divider}{OrgNumber}\n" +
                                      $"{"Name:",spacing1}{divider}{Name}\n" +
                                      $"{"address:", spacing1}{divider}{Adress}, {PostalCode} {City}\n" +
                                      $"\n" +
                                      $"Owned Accounts:";
                foreach (Account currentAccount in MyAccounts) {
                    returnString += $"\n{currentAccount.FullInfoAsString}";
                }
                return returnString;
            }
        }

    }
}
