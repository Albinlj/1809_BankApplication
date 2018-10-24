using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using _1809_BankApp.Transactions;
using static System.Console;

namespace _1809_BankApp
{
    class Program
    {
        public static Bank MyBank { get; } = new Bank();

        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("se");
            StartMenuLoop();
        }

        private static void StartMenuLoop()
        {
            PrintMenu();
            do
            {
                WriteLine();
                switch (QueryAndPrintAction())
                {
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
                    case Actions.ChangeCreditRoof:
                        ChangeCreditRoof();
                        break;
                    case Actions.ChangeCreditInterest:
                        ChangeCreditInterest();
                        break;
                    case Actions.ChangeDebitInterest:
                        ChangeDebitInterest();
                        break;
                    default:
                        WriteLine("Number does not correspond to a valid menu item. Try again!\n");
                        PrintMenu();
                        break;
                }
            } while (true);
        }

        private static void ChangeDebitInterest()
        {
            int accountId = ConsoleQueries.QueryInt("Input account ID: ");
            double debitInterest = ConsoleQueries.QueryDouble("Input new debitinterest: ");
            MyBank.AccountManager.GetAccountByAccountId(accountId).DebitInterestYearly = debitInterest;
        }

        private static void ChangeCreditInterest()
        {


            int accountId = ConsoleQueries.QueryInt("Input account ID: ");
            double creditInterest = ConsoleQueries.QueryDouble("Input new creditinterest: ");
            MyBank.AccountManager.GetAccountByAccountId(accountId).CreditInterestYearly = creditInterest;
        }

        private static void ChangeCreditRoof()
        {
            int accountId = ConsoleQueries.QueryInt("Input account ID: ");
            decimal creditRoof = ConsoleQueries.QueryDecimal("Input new creditroof: ");
            MyBank.AccountManager.GetAccountByAccountId(accountId).CreditRoof = creditRoof;
        }

        private static void SaveAndExit()
        {
            MyBank.DatabaseManager.Save();
            Environment.Exit(0);
        }

        private static void ApplyInterest()
        {
            foreach (Account account in MyBank.AccountManager.Accounts)
            {
                MyBank.TransactionManager.ApplyDailyInterest(account);
            }
        }

        private static void ShowAccountView()
        {
            int accountId = ConsoleQueries.QueryInt("Input ID of account: ");

            Account account = MyBank.AccountManager.GetAccountByAccountId(accountId);

            if (account == null)
            {
                WriteLine("No Account with that ID found.");
            }
            else
            {
                const int spacing1 = -30;
                const string divider = "-    ";
                string info = $"{"ID:",spacing1}{divider}{account.Id}\n" +
                              $"{"Owner Customer ID",spacing1}{divider}{account.OwnerId}\n" +
                              $"{"Balance",spacing1}{divider}{account.Balance:C}\n" +
                              $"{"Credit Roof:",spacing1}{divider}{account.CreditRoof}\n" +
                              $"{"Credit Interest:",spacing1}{divider}{account.CreditInterestYearly}\n" +
                              $"{"Debit Interest:",spacing1}{divider}{account.DebitInterestYearly}\n" +

                              $"\n" +
                              $"Today's Transactions:";
                foreach (Transaction transaction in MyBank.TransactionManager.GetTransactionsFromAccountId(account.Id))
                {
                    info += $"\n{transaction.InfoAsText}";
                    info += "\n";
                }

                WriteLine(info);
            }


        }


        internal static Actions QueryAndPrintAction()
        {
            Actions chosenAction = (Actions)ConsoleQueries.QueryInt("Input Number of Menu Choice: ");
            Console.WriteLine();
            if (Enum.IsDefined(typeof(Actions), chosenAction))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($" -== {Utility.PascalToTitlecase(chosenAction.ToString())} ==- ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return chosenAction;
        }

        private static void Transferral()
        {
            int sendingAccountId = ConsoleQueries.QueryInt("Input ID of sending account: ");
            int receivingAccountId = ConsoleQueries.QueryInt("Input ID of receiving account: ");
            decimal amount = ConsoleQueries.QueryDecimal("Input amount: ");

            Account sendingAccount = MyBank.AccountManager.GetAccountByAccountId(sendingAccountId);
            Account receivingAccount = MyBank.AccountManager.GetAccountByAccountId(receivingAccountId);

            if (sendingAccount == null || receivingAccount == null)
            {
                Console.WriteLine($"One of the input accounts does not exist.");
            }
            else
            {
                Transfer newTransfer = MyBank.TransactionManager.Transfer(sendingAccount, receivingAccount, amount);
                if (newTransfer == null)
                {
                    Console.WriteLine($"Transfer failed due to lack of funds.");
                }
                else
                {
                    Console.WriteLine($"Successfully transferred {amount:C} from {sendingAccountId} to {receivingAccountId}");
                }

            }

        }

        private static void Withdrawal()
        {
            int accountId = ConsoleQueries.QueryInt("Input ID of withdrawing account: ");
            decimal amount = ConsoleQueries.QueryDecimal("Input amount: ");

            Account withdrawingAccount = MyBank.AccountManager.GetAccountByAccountId(accountId);
            if (withdrawingAccount == null)
            {
                Console.WriteLine($"Failure to Withdraw {amount:C} from account {accountId}");
            }
            else
            {
                Withdrawal newWithdrawal = MyBank.TransactionManager.Withdraw(withdrawingAccount, amount);
                if (newWithdrawal == null)
                {
                    Console.WriteLine($"Withdrawal failed due to lack of funds.");
                }
                else
                {
                    Console.WriteLine($"Successfully Withdrew {amount:C} from {accountId}");
                }
            }

        }

        private static void Deposit()
        {
            int accountId = ConsoleQueries.QueryInt("Input ID of receiving account: ");
            decimal amount = ConsoleQueries.QueryDecimal("Input amount: ");

            Account receivingAccount = MyBank.AccountManager.GetAccountByAccountId(accountId);
            if (receivingAccount == null)
            {
                Console.WriteLine($"Failure to deposit {amount:C} to account {accountId}");
            }
            else
            {
                Deposit newDeposit = MyBank.TransactionManager.Deposit(receivingAccount, amount);
                Console.WriteLine($"Successfully deposited {amount:C} to {accountId}");
            }
        }

        private static void CreateCustomer()
        {
            Customer newCustomer = MyBank.CustomerManager.CreateNewCustomer();

            newCustomer.Name = ConsoleQueries.QueryString("Input name: ");
            newCustomer.OrgNumber = ConsoleQueries.QueryLong("Input organization number: ");
            if (newCustomer.OrgNumber.ToString().Length != 10)
            {
                Console.WriteLine("Orgnumber has to be 10 numbers. Aborting.");
                return;
            }

            newCustomer.Adress = ConsoleQueries.QueryString("Input street and number: ");
            newCustomer.City = ConsoleQueries.QueryString("Input city: ");
            newCustomer.Region = ConsoleQueries.QueryString("Input region: ");
            newCustomer.PostalCode = ConsoleQueries.QueryString("Input postal Code: ");
            newCustomer.Country = ConsoleQueries.QueryString("Input country: ");
            newCustomer.PhoneNumber = ConsoleQueries.QueryString("Input phone number: ");


            MyBank.AccountManager.AddAccount(newCustomer.ID);
        }

        private static void DeleteAccount()
        {
            int inputAccountId = ConsoleQueries.QueryInt("Input Account ID: ");
            bool wasSuccessful = MyBank.AccountManager.DeleteAccount(inputAccountId);
            if (wasSuccessful)
            {
                WriteLine("Successfully deleted account.");
            }
            else
            {
                WriteLine("Cannot delete Account.");
            }
        }

        private static void CreateAccount()
        {
            int inputAccountId = ConsoleQueries.QueryInt("Input Customer ID of Owner: ");
            MyBank.AccountManager.AddAccount(inputAccountId);
        }

        private static void DeleteCustomer()
        {
            int inputAccountId = ConsoleQueries.QueryInt("Input Customer ID: ");
            bool wasSuccessful = MyBank.CustomerManager.DeleteCustomer(inputAccountId);
            if (wasSuccessful)
            {
                WriteLine("Successfully deleted customer.");
            }
            else
            {
                WriteLine("Customer still has accounts - Can not delete customer.");
            }
        }

        private static void ShowCustomerView()
        {
            int inputCustomerId = ConsoleQueries.QueryInt("Input customer or account ID: ");
            WriteLine();

            Customer foundCustomer = MyBank.CustomerManager.GetCustomerById(inputCustomerId);
            if (foundCustomer == null)
            {
                Account foundAccount = MyBank.AccountManager.GetAccountByAccountId(inputCustomerId);
                if (foundAccount == null)
                {
                    WriteLine("No customer with that ID found.");

                }
                else
                {
                    foundCustomer = MyBank.CustomerManager.GetCustomerById(foundAccount.OwnerId);
                    WriteLine(foundCustomer.FullInfoAsString + "\n");
                    WriteLine($"{"Total Balance: "}{MyBank.AccountManager.GetAccountsByCustomerId(foundCustomer.ID).Sum(x => x.Balance):C}\n");

                }
            }
            else
            {
                WriteLine(foundCustomer.FullInfoAsString + "\n");
                WriteLine($"{"Total Balance: "}{MyBank.AccountManager.GetAccountsByCustomerId(foundCustomer.ID).Sum(x => x.Balance):C}\n");

            }
        }

        private static void SearchCustomer()
        {
            string input = ConsoleQueries.QueryString("Input name or city: ").ToUpper();
            WriteLine();
            List<Customer> foundCustomers = MyBank.CustomerManager.SearchByNameOrCity(input);
            if (foundCustomers.Count == 0)
            {
                WriteLine("No customers found!");
            }
            else
            {
                foreach (Customer customer in foundCustomers)
                {
                    WriteLine($"{customer.ID}: {customer.Name}");
                }
            }
        }

        public static void PrintMenu()
        {
            for (int i = 0; i < Enum.GetNames(typeof(Actions)).Length; i++)
            {
                Console.WriteLine($"{i}) {Utility.PascalToTitlecase(Enum.GetName(typeof(Actions), i))}");
            }
        }
    }
}
