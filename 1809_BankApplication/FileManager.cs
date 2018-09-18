using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace _1809_BankApplication {
    class Database {


        public static List<string[]> LoadCustomers() {
            List<string[]> customerInfoList = new List<string[]>();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            path += @"\Data\bankdata-small.txt";

            string[] strings = File.ReadAllLines(path);

            int customerCount = int.Parse(strings[0]);
            for (int i = 1; i <= customerCount; i++) {
                customerInfoList.Add(strings[i].Split(';'));
            }
            return customerInfoList;
        }

        internal static List<string[]> LoadAccounts() {
            List<string[]> accountInfoList = new List<string[]>();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            path += @"\Data\bankdata-small.txt";

            string[] strings = File.ReadAllLines(path);

            int customerCount = int.Parse(strings[0]);
            int accountCount = int.Parse(strings[customerCount + 1]);
            for (int i = customerCount+ 2; i <= customerCount + 1 + accountCount; i++) {
                accountInfoList.Add(strings[i].Split(';'));
            }
            return accountInfoList;
        }
    }
}