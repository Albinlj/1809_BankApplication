using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class TransactionManager {
        public List<Transaction> Transactions { get; set; }

        public Transaction CreateTransaction() {
            Transaction newTransaction = new Transaction();
            Transactions.Add(newTransaction);
                            return newTransaction;
        }

        public bool PerformTransaction(Account accountSending, Account accountReceiving, decimal amount, ref Transaction newTransaction) {
            if (accountSending.Balance - amount < accountSending.CreditRoof) {
                //Not allowed
                return false;
            }

            newTransaction = CreateTransaction();
            newTransaction.AccountSenderId = accountSending.Id;
            newTransaction.AccountReceiverId = accountReceiving.Id;
            newTransaction.CustomerSenderId = accountSending.OwnerId;
            newTransaction.CustomerReceiverId = accountReceiving.OwnerId;
            newTransaction.TimeOfTransaction = DateTime.Now;
            accountReceiving.Balance += amount;
            accountSending.Balance -= amount;

            Program.DatabaseManager.AddTransaction(newTransaction);
            Program.DatabaseManager.UpdateAccounts(new List<Account>() { accountSending, accountReceiving });

            return true;
        }

        public bool PerformDeposit( Account accountReceiving, decimal amount, ref Deposit newDeposit) {

            newTransaction = CreateTransaction();
            newTransaction.AccountSenderId = accountSending.Id;
            newTransaction.AccountReceiverId = accountReceiving.Id;
            newTransaction.CustomerSenderId = accountSending.OwnerId;
            newTransaction.CustomerReceiverId = accountReceiving.OwnerId;
            newTransaction.TimeOfTransaction = DateTime.Now;
            accountReceiving.Balance += amount;
            accountSending.Balance -= amount;

            Program.DatabaseManager.AddTransaction(newTransaction);
            Program.DatabaseManager.UpdateAccounts(new List<Account>() { accountSending, accountReceiving });

            return true;
        }

    }
}
