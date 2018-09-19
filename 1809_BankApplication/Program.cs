using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1809_BankApplication.Transactions;
using static System.Console;

namespace _1809_BankApplication {
    class Program {
        public static Bank MyBank { get; } = new Bank();

        private static void Main(string[] args) {
            //Load Files
            PrintMenu();
            do {
                WriteLine();
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
            WriteLine("* Show Account View *");
            Write("Input ID of account: ");
            int accountId = QueryInt();

            Account receivingAccount = MyBank.AccountManager.GetAccountByAccountId(accountId);
        }

        private static int QueryInt() {
            int output;
            bool wasSuccess;
            do {
                wasSuccess = int.TryParse(ReadLine(), out output);
                if (wasSuccess == false) Console.WriteLine("Not a valid integer. Try again!");

            } while (wasSuccess == false);
            return output;
        }

        private static void Transaction() {
            Write("* Transaction *\n" +
                  "Input ID of sending account: ");
            int sendingAccountId = QueryInt();
            Write("Input ID of receiving account: ");
            int receivingAccountId = QueryInt();
            Write("Input amount: ");
            int amount = QueryInt();

            Account sendingAccount = MyBank.AccountManager.GetAccountByAccountId(sendingAccountId);
            Account receivingAccount = MyBank.AccountManager.GetAccountByAccountId(receivingAccountId);

            Transfer newTransfer = MyBank.TransactionManager.Transfer(sendingAccount, receivingAccount, amount);
            Console.WriteLine(newTransfer != null
                ? $"Successfully transferred {amount:C} from {sendingAccountId} to {receivingAccountId}"
                : $"Failed to execute transaction");
        }

        private static void Withdrawal() {
            Write("* Withdrawal *\n" +
                  "Input ID of withdrawing account: ");
            int accountId = QueryInt();
            Write("Input amount: ");
            int amount = QueryInt();

            Account withdrawingAccount = MyBank.AccountManager.GetAccountByAccountId(accountId);

            Withdrawal newWithdrawal = MyBank.TransactionManager.Withdraw(withdrawingAccount, amount);
            Console.WriteLine(newWithdrawal != null
                ? $"Successfully deposited {amount:C} to {accountId}"
                : $"Failure to deposit to account {accountId}");
        }

        private static void Deposit() {
            Write("* Deposit *\n" +
                  "Input ID of receiving account: ");
            int accountId = QueryInt();
            Write("Input amount: ");
            int amount = QueryInt();

            Account receivingAccount = MyBank.AccountManager.GetAccountByAccountId(accountId);

            Deposit newDeposit = MyBank.TransactionManager.Deposit(receivingAccount, amount);
            Console.WriteLine(newDeposit != null
                ? $"Successfully deposited {amount:C} to {accountId}"
                : $"Failure to deposit to account {accountId}");
        }

        private static void CreateCustomer() {
            Write("* Create Customer *\n");

            Customer newCustomer = MyBank.CustomerManager.CreateNewCustomer();

            Write("Input name: ");
            newCustomer.Name = ReadLine();
            Write("Input organization number: ");
            newCustomer.OrgNumber = QueryInt();
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
            MyBank.AccountManager.DeleteAccount(QueryInt());
        }

        private static void CreateAccount() {
            Write("* Create Account *\n" +
                  "Input Customer ID of Owner: ");
            string input = ReadLine();
            MyBank.AccountManager.AddAccount(QueryInt());
        }

        private static void DeleteCustomer() {
            Write("* Delete Customer *\n" +
                  " Input Customer ID: ");
            string input = ReadLine();
            MyBank.CustomerManager.DeleteCustomer(QueryInt());
        }

        private static void ShowCustomerView() {
            Write("* Show Customer View *\n" +
                  "Input customer ID: ");
            string input = ReadLine();
            WriteLine();
            Customer foundCustomer = MyBank.CustomerManager.GetCustomerById(QueryInt());
            if (foundCustomer != null) {
                WriteLine(foundCustomer.FullInfoAsString + "\n");
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
