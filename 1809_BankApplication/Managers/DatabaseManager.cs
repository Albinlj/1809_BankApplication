using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace _1809_BankApp {
    public class DatabaseManager {
        public static string LoadPath { get; private set; }
        public static string DataPath { get; private set; }
        public Bank MyBank { get; }

        public DatabaseManager(Bank bank) {
            MyBank = bank;
            DataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName + @"\Data\";
            LoadPath = DataPath + @"bankdata.txt";
        }


        public static List<string[]> LoadCustomersInfo() {
            List<string[]> customerInfoList = new List<string[]>();

            string[] strings = File.ReadAllLines(LoadPath);

            int customerCount = int.Parse(strings[0]);
            Console.WriteLine($"Total customers: {customerCount}");

            for (int i = 1; i <= customerCount; i++) {
                customerInfoList.Add(strings[i].Split(';'));
            }

            return customerInfoList;
        }

        internal static List<string[]> LoadAccountsInfo() {
            List<string[]> accountInfoList = new List<string[]>();

            string[] strings = File.ReadAllLines(LoadPath);

            int customerCount = int.Parse(strings[0]);
            int accountCount = int.Parse(strings[customerCount + 1]);
            Console.WriteLine($"Total accounts: {accountCount}");
            for (int i = customerCount + 2; i <= customerCount + 1 + accountCount; i++) {
                accountInfoList.Add(strings[i].Split(';'));
            }

            decimal totalBalance = accountInfoList.Sum(x => decimal.Parse(x[2], CultureInfo.InvariantCulture));
            Console.WriteLine($"Total balance: {totalBalance}\n");


            return accountInfoList;
        }

        public void Save() {
            string filename = $"{DateTime.Now:yyyyMMdd-HHmm}" + ".txt";
            StreamWriter writer = new StreamWriter(DataPath + filename);
            writer.WriteLine(MyBank.CustomerManager.Customers.Count);
            foreach (Customer customer in MyBank.CustomerManager.Customers) {
                string[] customerInfo = new string[9];
                customerInfo[0] = customer.ID.ToString();
                string orgNum = customer.OrgNumber.ToString();
                string orgNumWithDash = orgNum.Substring(0, 6) + "-" + orgNum.Substring(5, 4);
                customerInfo[1] = orgNumWithDash;
                customerInfo[2] = customer.Name;
                customerInfo[3] = customer.Adress;
                customerInfo[4] = customer.City;
                customerInfo[5] = customer.PostalCode;
                customerInfo[6] = customer.Country;
                customerInfo[7] = customer.PhoneNumber;

                writer.WriteLine(string.Join(";", customerInfo));
            }
            writer.WriteLine(MyBank.AccountManager.Accounts.Count);
            foreach (Account account in MyBank.AccountManager.Accounts) {
                string[] accountInfo = new string[3];
                accountInfo[0] = account.Id.ToString();
                accountInfo[1] = account.OwnerId.ToString();
                accountInfo[2] = account.Balance.ToString();

                writer.WriteLine(string.Join(";", accountInfo));
            }
            writer.Close();
        }




        public void WriteTransactionLog(Transaction transaction) {
            StreamWriter writer = new StreamWriter(DataPath + @"transactionlog.txt", true);
            writer.WriteLine(transaction.InfoAsText + "\n");
            writer.Close();
        }
    }
}