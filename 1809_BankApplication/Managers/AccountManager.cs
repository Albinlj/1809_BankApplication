using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApp
{
    public class AccountManager
    {
        public Bank MyBank { get; }
        public List<Account> Accounts { get; } = new List<Account>();

        public AccountManager(Bank bank)
        {
            MyBank = bank;
        }

        public void LoadAccounts()
        {
            List<string[]> accountInfos = DatabaseManager.LoadAccountsInfo();
            foreach (string[] info in accountInfos)
            {
                Account newAccount = new Account()
                {
                    Id = int.Parse(info[0]),
                    OwnerId = int.Parse(info[1]),
                    Balance = decimal.Parse(info[2], CultureInfo.InvariantCulture)
                };


                Accounts.Add(newAccount);
                MyBank.CustomerManager.GetCustomerById(newAccount.OwnerId).MyAccounts.Add(newAccount);
            }
        }

        public IEnumerable<Account> GetAccountsByCustomerId(int customerId)
        {
            return (Accounts.Where(account => account.OwnerId == customerId));
        }

        public Account GetAccountByAccountId(int accountToGetId)
        {
            foreach (Account currentAccount in Accounts)
            {
                if (currentAccount.Id == accountToGetId)
                {
                    return currentAccount;
                }
            }
            return null;
        }

        public bool DeleteAccount(int accountToDeleteId)
        {
            Account accountToDelete = GetAccountByAccountId(accountToDeleteId);
            if (accountToDelete != null && accountToDelete.Balance == 0)
            {
                Accounts.Remove(accountToDelete);
                MyBank.CustomerManager.GetCustomerById(accountToDelete.OwnerId).MyAccounts.Remove(accountToDelete);
                accountToDelete.OwnerId = 0;
                return true;
            }
            return false;
        }

        internal void AddAccount(int ownerId)
        {
            Customer ownerOfNewAccount = MyBank.CustomerManager.GetCustomerById(ownerId);

            if (ownerOfNewAccount != null)
            {

                int currentMaxId = 00000;
                foreach (Account account in Accounts)
                {
                    if (account.Id > currentMaxId)
                    {
                        currentMaxId = account.Id;
                    }
                }

                Account newAccount = new Account()
                {
                    Id = currentMaxId + 1,
                    OwnerId = ownerId,
                    Balance = 0
                };

                Accounts.Add(newAccount);

                ownerOfNewAccount.MyAccounts.Add(newAccount);
            }
            else
            {
                Console.WriteLine("No customer with that ID found");
            }
        }


    }
}
