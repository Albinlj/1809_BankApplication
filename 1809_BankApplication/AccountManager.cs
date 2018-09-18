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

        public List<Account> GetAccountsByCustomerID(int customerId) {
            List<Account> returnList = new List<Account>();
            foreach (Account account in accounts) {
                if (account.OwnerId == customerId) {
                    returnList.Add(account);
                }
            }
            return returnList;
        }

        public Account GetAccountByAccountID(int accountToGetId) {
            foreach (Account currentAccount in accounts) {
                if (currentAccount.Id == accountToGetId) {
                    return currentAccount;
                }
            }
            return null;
        }

        public bool DeleteAccount(int accountToDeleteId) {
            Account accountToDelete = GetAccountByAccountID(accountToDeleteId);
            if (accountToDelete != null) {
                accounts.Remove(accountToDelete);
                Program.customerManager.GetCustomerByID(accountToDelete.OwnerId).Accounts.Remove(accountToDelete);
                accountToDelete.OwnerId = 0;
                return true;
            }
            return false;
        }

        internal void AddAccount(int ownerID) {
            int currentMaxID = 00000;
            foreach (Account account in accounts) {
                if (account.Id > currentMaxID) {
                    currentMaxID = account.Id;
                }
            }

            Account newAccount = new Account() {
                Id = currentMaxID + 1,
                OwnerId = ownerID,
                Balance = 0
            };

            accounts.Add(newAccount);
            Program.customerManager.GetCustomerByID(ownerID).Accounts.Add(newAccount);
        }
    }
}
