using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class Program {
        static FileManager fileManager = new FileManager();
        static CustomerManager customerManager = new CustomerManager();
        static AccountManager accountManager = new AccountManager();
        static TransactionManager transactionManager = new TransactionManager();

        static void Main(string[] args) {
            //Load Files
            customerManager.LoadCustomers();
            accountManager.LoadAccounts();

            do {
                switch (Input.Query()) {
                    case Actions.SaveAndExit:
                        break;
                    case Actions.SearchCustomer:
                        break;
                    case Actions.ShowCustomerView:
                        break;
                    case Actions.CreateCustomer:
                        break;
                    case Actions.DeleteCustomer:
                        break;
                    case Actions.CreateAccount:
                        break;
                    case Actions.DeleteAccount:
                        break;
                    case Actions.Deposit:
                        break;
                    case Actions.Withdrawal:
                        break;
                    case Actions.Transaction:
                        break;
                    case Actions.ApplyInterest:
                    default:
                        break;
                }

            } while (true);



        }
    }
}
