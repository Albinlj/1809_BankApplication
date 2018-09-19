using System;
using System.Collections.Generic;
using _1809_BankApplication.Transactions;

namespace _1809_BankApplication {
    class TransactionManager {
        public List<Transaction> Transactions { get; set; }

        public T CreateTransaction<T>() where T : Transaction, new() {
            T newTransaction = new T();
            Transactions.Add(newTransaction);
            return newTransaction;
        }

        public Transfer TryTransfer(Account sendingAccount, Account receivingAccount, decimal amount) {
            if (!HasEnoughFunds(sendingAccount, amount)) return null;

            var newTransfer = CreateTransaction<Transfer>();

            newTransfer.AccountSenderId = sendingAccount.Id;
            newTransfer.AccountReceiverId = receivingAccount.Id;
            newTransfer.CustomerSenderId = sendingAccount.OwnerId;
            newTransfer.CustomerReceiverId = receivingAccount.OwnerId;
            newTransfer.TimeOfTransaction = DateTime.Now;
            newTransfer.Amount = amount;
            receivingAccount.Balance += amount;
            sendingAccount.Balance -= amount;

            Program.DatabaseManager.AddTransaction(newTransfer);
            Program.DatabaseManager.UpdateAccount(new List<Account>() {sendingAccount, receivingAccount});

            return newTransfer;
        }

        public Deposit Deposit( Account receivingAccount, decimal amount) {

            var newDeposit = CreateTransaction<Deposit>();

            newDeposit.AccountReceiverId = receivingAccount.Id;
            newDeposit.CustomerReceiverId = receivingAccount.OwnerId;
            newDeposit.TimeOfTransaction = DateTime.Now;
            newDeposit.Amount = amount;
            receivingAccount.Balance += amount;

            Program.DatabaseManager.AddTransaction(newDeposit);
            Program.DatabaseManager.UpdateAccount(receivingAccount);

            return newDeposit;
        }

        public Withdrawal Withdraw(Account withdrawingAccount, decimal amount) {
            if (!HasEnoughFunds(withdrawingAccount, amount)) return null;

            var newTransfer = CreateTransaction<Withdrawal>();

            newTransfer.AccountReceiverId = withdrawingAccount.Id;
            newTransfer.CustomerReceiverId = withdrawingAccount.OwnerId;
            newTransfer.TimeOfTransaction = DateTime.Now;
            newTransfer.Amount = amount;
            withdrawingAccount.Balance -= amount;

            Program.DatabaseManager.AddTransaction(newTransfer);
            Program.DatabaseManager.UpdateAccount(withdrawingAccount);

            return newTransfer;
        }


        private static bool HasEnoughFunds(Account accountSending, decimal amount) {
            return accountSending.Balance - amount >= accountSending.CreditRoof;
        }
    }
}
