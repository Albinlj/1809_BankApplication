using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _1809_BankApplication {
    class Program {
        public static Bank MyBank { get; } = new Bank();

        private static void Main(string[] args) {
            //Load Files
            PrintMenu();
            do {
                switch (Menu.QueryAction()) {

                    case Actions.SaveAndExit:
                        break;
                    case Actions.SearchCustomer:
                        SearchCustomer();
                        break;
                    case Actions.ShowCustomerView:
                        ShowCustomerView();
                        break;
                    case Actions.CreateCustomer:
                        CreateCustomer();
                        break;
                    case Actions.DeleteCustomer:
                        DeleteCustomer();
                        break;
                    case Actions.CreateAccount:
                        CreateAccount();
                        break;
                    case Actions.DeleteAccount:
                        DeleteAccount();
                        break;
                    case Actions.Deposit:
                        Deposit();
                        break;
                    case Actions.Withdrawal:
                        Withdrawal();
                        break;
                    case Actions.Transaction:
                        Transaction();
                        break;
                    case Actions.ShowAccountView:
                        ShowAccountView();
                        break;
                    case Actions.ApplyInterest:
                        ApplyInterest();
                        break;
                    default:
                        Console.WriteLine("NEIN");
                        break;
                }

            } while (true);



        }

        private static void ApplyInterest() {
            throw new NotImplementedException();
        }

        private static void ShowAccountView() {
            throw new NotImplementedException();
        }

        private static void Transaction() {
            throw new NotImplementedException();
        }

        private static void Withdrawal() {
            throw new NotImplementedException();
        }

        private static void Deposit() {
            throw new NotImplementedException();
        }

        private static void CreateCustomer() {
            Write("* Create Customer *\n");

            Customer newCustomer = MyBank.CustomerManager.CreateCustomer();

            Write("Input name: ");
            newCustomer.Name = ReadLine();
            Write("Input organization number: ");
            newCustomer.OrgNumber = Int64.Parse(ReadLine());
            Write("Input street and number: ");
            newCustomer.Adress = ReadLine();
            Write("Input city: ");
            newCustomer.City = ReadLine();
            Write("Input region: ");
            newCustomer.Region = ReadLine();
            Write("Input postal Code: ");
            newCustomer.PostalCode = ReadLine();
            Write("Input country: ");
            newCustomer.Country = ReadLine();
            Write("Input phone number: ");
            newCustomer.PhoneNumber = ReadLine();

            MyBank.AccountManager.AddAccount(newCustomer.ID);
        }

        private static void DeleteAccount() {
            Write("* Delete Account *\n" +
                "Input Account ID: ");
            string input = ReadLine();
            MyBank.AccountManager.DeleteAccount(Int32.Parse(input));
        }

        private static void CreateAccount() {
            Write("* Create Account *\n" +
                "Input Customer ID of Owner: ");
            string input = ReadLine();
            MyBank.AccountManager.AddAccount(Int32.Parse(input));
        }

        private static void DeleteCustomer() {
            Write("* Delete Customer *\n" +
                " Input Customer ID: ");
            string input = ReadLine();
            MyBank.CustomerManager.DeleteCustomer(Int32.Parse(input));
        }

        private static void ShowCustomerView() {
            Write("* Show Customer View *\n" +
                "Input customer ID: ");
            string input = ReadLine();
            WriteLine();
            Customer foundCustomer = MyBank.CustomerManager.GetCustomerById(Int32.Parse(input));
            if (foundCustomer != null) {
                WriteLine(foundCustomer.FullInfoAsString);
                foreach (Account account in MyBank.AccountManager.GetAccountsByCustomerId(Int32.Parse(input))) {
                    WriteLine(account.FullInfoAsString);
                }
                WriteLine();
            }
            else {
                WriteLine("No customer with that ID found");
            }
        }

        private static void SearchCustomer() {
            WriteLine("* Search Customer *\n" +
                      "Input name or city: ");
            string input = ReadLine().ToUpper();
            WriteLine();
            List<Customer> foundCustomers = MyBank.CustomerManager.SearchByNameOrCity(input);
            foreach (Customer customer in foundCustomers) {
                WriteLine($"{customer.ID}: {customer.Name}");
            }
        }

        public static void PrintMenu() {
            for (int i = 0; i < Enum.GetNames(typeof(Actions)).Length; i++) {
                Console.WriteLine($"{i}) {Utility.PascalToSentence(Enum.GetName(typeof(Actions), i))}");
            }
        }
    }
}
