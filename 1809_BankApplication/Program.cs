using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _1809_BankApp.Transactions;
using static System.Console;

namespace _1809_BankApp {
    class Program {
        public static Bank MyBank { get; } = new Bank();

        private static void Main(string[] args) {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("se");
            PrintMenu();
            do {
                WriteLine();
                switch (QueryAndPrintAction()) {
                    case Actions.SaveAndExit:
                        SaveAndExit();
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
                    case Actions.Transferral:
                        Transferral();
                        break;
                    case Actions.ShowAccountView:
                        ShowAccountView();
                        break;
                    case Actions.ApplyInterest:
                        ApplyInterest();
                        break;
                    default:
                        WriteLine("Number does not correspond to a valid menu item. Try again!\n");
                        PrintMenu();
                        break;
                }
            } while (true);
        }

        private static void SaveAndExit() {
            MyBank.DatabaseManager.Save();
        }

        private static void ApplyInterest() {
            foreach (Account account in MyBank.AccountManager.Accounts) {
                InterestApplier.ApplyDailyInterest(account);
            }
        }

        private static void ShowAccountView() {
            int accountId = QueryInt("Input ID of account: ");

            Account receivingAccount = MyBank.AccountManager.GetAccountByAccountId(accountId);

            if (receivingAccount == null) {
                WriteLine("No Account with that ID found.");
            }
            else {
                const int spacing1 = -30;
                const string divider = "-    ";
                string info = $"{"ID:",spacing1}{divider}{receivingAccount.Id}\n" +
                              $"{"Owner Customer ID",spacing1}{divider}{receivingAccount.OwnerId}\n" +
                              $"\n" +
                              $"Today's Transactions:";
                foreach (Transaction transaction in MyBank.TransactionManager.GetTransactionsFromAccountId(receivingAccount.Id)) {
                    info += $"\n{transaction.InfoAsText}";
                    info += "\n";
                }

                WriteLine(info);
            }


        }

        private static string QueryString(string message) {
            Console.Write(message);
            return ReadLine();
        }


        private static int QueryInt(string message) {
            Write(message);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            int output;
            bool wasSuccess;
            do {
                wasSuccess = int.TryParse(ReadLine(), out output);
                if (wasSuccess == false) Console.WriteLine("Not a valid integer. Try again!");
            } while (wasSuccess == false);

            Console.ForegroundColor = ConsoleColor.Gray;

            return output;
        }

        internal static Actions QueryAndPrintAction() {
            Actions chosenAction = (Actions)QueryInt("Input Number of Menu Choice: ");
            Console.WriteLine();
            if (Enum.IsDefined(typeof(Actions), chosenAction)) {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($" -== {Utility.PascalToTitlecase(chosenAction.ToString())} ==- ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return chosenAction;
        }

        private static void Transferral() {
            int sendingAccountId = QueryInt("Input ID of sending account: ");
            int receivingAccountId = QueryInt("Input ID of receiving account: ");
            int amount = QueryInt("Input amount: ");

            Account sendingAccount = MyBank.AccountManager.GetAccountByAccountId(sendingAccountId);
            Account receivingAccount = MyBank.AccountManager.GetAccountByAccountId(receivingAccountId);

            if (sendingAccount == null || receivingAccount == null) {
                Console.WriteLine($"One of the input accounts does not exist.");
            }
            else {
                Transfer newTransfer = MyBank.TransactionManager.Transfer(sendingAccount, receivingAccount, amount);
                if (newTransfer == null) {
                    Console.WriteLine($"Transfer failed due to lack of funds.");
                }
                else {
                    Console.WriteLine($"Successfully transferred {amount:C} from {sendingAccountId} to {receivingAccountId}");
                }

            }

        }

        private static void Withdrawal() {
            int accountId = QueryInt("Input ID of withdrawing account: ");
            int amount = QueryInt("Input amount: ");

            Account withdrawingAccount = MyBank.AccountManager.GetAccountByAccountId(accountId);
            if (withdrawingAccount == null) {
                Console.WriteLine($"Failure to Withdraw {amount:C} from account {accountId}");
            }
            else {
                Withdrawal newWithdrawal = MyBank.TransactionManager.Withdraw(withdrawingAccount, amount);
                if (newWithdrawal == null) {
                    Console.WriteLine($"Withdrawal failed due to lack of funds.");
                }
                else {
                    Console.WriteLine($"Successfully Withdrew {amount:C} from {accountId}");
                }
            }

        }

        private static void Deposit() {
            int accountId = QueryInt("Input ID of receiving account: ");
            int amount = QueryInt("Input amount: ");

            Account receivingAccount = MyBank.AccountManager.GetAccountByAccountId(accountId);
            if (receivingAccount == null) {
                Console.WriteLine($"Failure to deposit {amount:C} to account {accountId}");
            }
            else {
                Deposit newDeposit = MyBank.TransactionManager.Deposit(receivingAccount, amount);
                Console.WriteLine($"Successfully deposited {amount:C} to {accountId}");
            }
        }

        private static void CreateCustomer() {
            Customer newCustomer = MyBank.CustomerManager.CreateNewCustomer();

            newCustomer.Name = QueryString("Input name: ");
            newCustomer.OrgNumber = QueryInt("Input organization number: ");
            if (newCustomer.OrgNumber.ToString().Length != 10) {
                Console.WriteLine("Orgnumber has to be 10 numbers. Aborting.");
                return;
            }
            newCustomer.Adress = QueryString("Input street and number: ");
            newCustomer.City = QueryString("Input city: ");
            newCustomer.Region = QueryString("Input region: ");
            newCustomer.PostalCode = QueryString("Input postal Code: ");
            newCustomer.Country = QueryString("Input country: ");
            newCustomer.PhoneNumber = QueryString("Input phone number: ");


            MyBank.AccountManager.AddAccount(newCustomer.ID);
        }

        private static void DeleteAccount() {
            int inputAccountId = QueryInt("Input Account ID: ");
            MyBank.AccountManager.DeleteAccount(inputAccountId);
        }

        private static void CreateAccount() {
            int inputAccountId = QueryInt("Input Customer ID of Owner: ");
            MyBank.AccountManager.AddAccount(inputAccountId);
        }

        private static void DeleteCustomer() {
            int inputAccountId = QueryInt("Input Customer ID: ");
            MyBank.CustomerManager.DeleteCustomer(inputAccountId);
        }

        private static void ShowCustomerView() {
            int inputCustomerId = QueryInt("Input customer ID: ");
            WriteLine();
            Customer foundCustomer = MyBank.CustomerManager.GetCustomerById(inputCustomerId);
            if (foundCustomer == null) {
                WriteLine("No customer with that ID found.");
            }
            else {
                WriteLine(foundCustomer.FullInfoAsString + "\n");
            }
        }

        private static void SearchCustomer() {
            string input = QueryString("Input name or city: ").ToUpper();
            WriteLine();
            List<Customer> foundCustomers = MyBank.CustomerManager.SearchByNameOrCity(input);
            if (foundCustomers.Count == 0) {
                WriteLine("No customers found!");
            }
            else {
                foreach (Customer customer in foundCustomers) {
                    WriteLine($"{customer.ID}: {customer.Name}");
                }
            }
        }

        public static void PrintMenu() {
            for (int i = 0; i < Enum.GetNames(typeof(Actions)).Length; i++) {
                Console.WriteLine($"{i}) {Utility.PascalToTitlecase(Enum.GetName(typeof(Actions), i))}");
            }
        }
    }
}
