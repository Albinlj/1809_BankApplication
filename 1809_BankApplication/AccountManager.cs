using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class AccountManager {
        public Bank MyBank { get; }
        public static AccountManager instance;
        public List<Account> Accounts { get; } = new List<Account>();

        public AccountManager(Bank bank) {
            MyBank = bank;
        }

        public void LoadAccounts() {
            List<string[]> accountInfos = DatabaseManager.LoadAccounts();
            foreach (string[] info in accountInfos) {
                Accounts.Add(new Account() {
                    Id = int.Parse(info[0]),
                    OwnerId = int.Parse(info[1]),
                    Balance = decimal.Parse(info[2])
                });
            }
        }

        public List<Account> GetAccountsByCustomerId(int customerId) {
            List<Account> returnList = new List<Account>();
            foreach (Account account in Accounts) {
                if (account.OwnerId == customerId) {
                    returnList.Add(account);
                }
            }
            return returnList;
        }

        public Account GetAccountByAccountId(int accountToGetId) {
            foreach (Account currentAccount in Accounts) {
                if (currentAccount.Id == accountToGetId) {
                    return currentAccount;
                }
            }
            return null;
        }

        public bool DeleteAccount(int accountToDeleteId) {
            Account accountToDelete = GetAccountByAccountId(accountToDeleteId);
            if (accountToDelete != null && accountToDelete.Balance == 0) {
                Accounts.Remove(accountToDelete);
                MyBank.CustomerManager.GetCustomerById(accountToDelete.OwnerId).Accounts.Remove(accountToDelete);
                accountToDelete.OwnerId = 0;
                return true;
            }
            return false;
        }

        internal void AddAccount(int ownerId) {
            int currentMaxId = 00000;
            foreach (Account account in Accounts) {
                if (account.Id > currentMaxId) {
                    currentMaxId = account.Id;
                }
            }

            Account newAccount = new Account() {
                Id = currentMaxId + 1,
                OwnerId = ownerId,
                Balance = 0
            };

            Accounts.Add(newAccount);
            MyBank.CustomerManager.GetCustomerById(ownerId).Accounts.Add(newAccount);
        }
    }
}
