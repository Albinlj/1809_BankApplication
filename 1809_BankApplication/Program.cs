using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class Program {
        public static Database fileManager = new Database();
        public static CustomerManager customerManager = new CustomerManager();
        public static TransactionManager transactionManager = new TransactionManager();
        public static AccountManager accountManager = new AccountManager();

        private static void Main(string[] args) {
            //Load Files
            customerManager.LoadCustomers();
            accountManager.LoadAccounts();
            Input.PrintMenu();
            do {
                string input;
                switch (Input.Query()) {

                    case Actions.SaveAndExit:
                        break;

                    case Actions.SearchCustomer:
                        Console.WriteLine("* Search Customer *\n" +
                            "Input name or city: ");
                        input = Console.ReadLine().ToUpper();
                        Console.WriteLine();
                        List<Customer> foundCustomers = customerManager.SearchByNameOrCity(input);
                        foreach (Customer customer in foundCustomers) {
                            Console.WriteLine($"{customer.ID}: {customer.Name}");
                        }
                        break;

                    case Actions.ShowCustomerView:
                        Console.Write("* Show Customer View *\n" +
                            "Input customer ID: ");
                        input = Console.ReadLine();
                        Console.WriteLine();
                        Customer foundCustomer = customerManager.GetCustomerByID(int.Parse(input));
                        if (foundCustomer != null) {
                            Console.WriteLine(foundCustomer.FullInfoAsString);
                            foreach (Account account in accountManager.GetAccountsByCustomerID(int.Parse(input))) {
                                Console.WriteLine(account.FullInfoAsString);
                            }
                            Console.WriteLine();
                        }
                        else {
                            Console.WriteLine("No customer with that ID found");
                        }
                        break;


                    case Actions.CreateCustomer:
                        Console.Write("* Create Customer *");
                        customerManager.CreateCustomerFromQueries();
                        break;

                    case Actions.DeleteCustomer:
                        Console.Write("* Delete Customer *\n" +
                            " Input Customer ID: ");
                        input = Console.ReadLine();
                        customerManager.DeleteCustomer(int.Parse(input));
                        break;

                    case Actions.CreateAccount:
                        Console.Write("* Create Account *\n" +
                            "Input Customer ID of Owner: ");
                        input = Console.ReadLine();
                        accountManager.AddAccount(int.Parse(input));
                        break;

                    case Actions.DeleteAccount:
                        Console.Write("* Delete Account *\n" +
                            "Input Account ID: ");
                        input = Console.ReadLine();
                        accountManager.DeleteAccount(int.Parse(input)) ;
                        break;
                    case Actions.Deposit:
                        break;
                    case Actions.Withdrawal:
                        break;
                    case Actions.Transaction:
                        break;
                    case Actions.ShowAccountView:
                        break;
                    case Actions.ApplyInterest:
                    default:
                        break;
                }

            } while (true);



        }
    }
}
