using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class AccountManager {
        public static AccountManager instance;
        List<Account> accounts = new List<Account>();

        public AccountManager() {
            instance = this;
        }

        public void LoadAccounts() {
            List<string[]> accountInfos = Database.LoadAccounts();
            foreach (string[] info in accountInfos) {
                accounts.Add(new Account() {
                    Id = int.Parse(info[0]),
                    OwnerId = int.Parse(info[1]),
                    Balance = decimal.Parse(info[2])
                });
            }
        }

        public List<Account> GetAccountsByCustomerId(int customerId) {
            List<Account> returnList = new List<Account>();
            foreach (Account account in accounts) {
                if (account.OwnerId == customerId) {
                    returnList.Add(account);
                }
            }
            return returnList;
        }

        public Account GetAccountByAccountId(int accountToGetId) {
            foreach (Account currentAccount in accounts) {
                if (currentAccount.Id == accountToGetId) {
                    return currentAccount;
                }
            }
            return null;
        }

        public bool DeleteAccount(int accountToDeleteId) {
            Account accountToDelete = GetAccountByAccountId(accountToDeleteId);
            if (accountToDelete != null && accountToDelete.Balance == 0) {
                accounts.Remove(accountToDelete);
                Program.CustomerManager.GetCustomerById(accountToDelete.OwnerId).Accounts.Remove(accountToDelete);
                accountToDelete.OwnerId = 0;
                return true;
            }
            return false;
        }

        internal void AddAccount(int ownerId) {
            int currentMaxId = 00000;
            foreach (Account account in accounts) {
                if (account.Id > currentMaxId) {
                    currentMaxId = account.Id;
                }
            }

            Account newAccount = new Account() {
                Id = currentMaxId + 1,
                OwnerId = ownerId,
                Balance = 0
            };

            accounts.Add(newAccount);
            Program.CustomerManager.GetCustomerById(ownerId).Accounts.Add(newAccount);
        }
    }
}
