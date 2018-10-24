using System;
using System.Collections.Generic;
using System.Linq;
using _1809_BankApp.Transactions;

namespace _1809_BankApp {
    public class TransactionManager {
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public Bank MyBank { get; }
        public TransactionManager(Bank bank) {
            MyBank = bank;
        }

        public TransactionManager() {
        }

        public T CreateTransaction<T>() where T : Transaction, new() {
            T newTransaction = new T();
            Transactions.Add(newTransaction);
            return newTransaction;
        }

        public InterestApplication ApplyDailyInterest(Account account)
        {
            var newInterestApplication = CreateTransaction<InterestApplication>();


            double multiplier = account.Balance > 0 ? account.DebitInterestYearly + 1 : account.CreditInterestYearly + 1;
            decimal addedAmount = account.Balance * ((decimal)Math.Pow((multiplier), 1 / 365d) - 1);
            account.Balance += addedAmount;

            newInterestApplication.InterestRate = multiplier - 1;
            newInterestApplication.AccountReceiverId = account.Id;
            newInterestApplication.CustomerReceiverId = account.OwnerId;
            newInterestApplication.Amount = addedAmount;
            newInterestApplication.TimeOfTransaction = DateTime.Now;

            MyBank?.DatabaseManager.WriteTransactionLog(newInterestApplication);


            return newInterestApplication;
        }


        public Transfer Transfer(Account sendingAccount, Account receivingAccount, decimal amount) {
            if (!HasEnoughFunds(sendingAccount, amount) || amount < 0) return null;

            var newTransfer = CreateTransaction<Transfer>();

            newTransfer.AccountSenderId = sendingAccount.Id;
            newTransfer.AccountReceiverId = receivingAccount.Id;
            newTransfer.CustomerSenderId = sendingAccount.OwnerId;
            newTransfer.CustomerReceiverId = receivingAccount.OwnerId;
            newTransfer.TimeOfTransaction = DateTime.Now;
            newTransfer.Amount = amount;
            receivingAccount.Balance += amount;
            sendingAccount.Balance -= amount;


            MyBank?.DatabaseManager.WriteTransactionLog(newTransfer);

            return newTransfer;
        }

        public Deposit Deposit(Account receivingAccount, decimal amount) {

            if (amount < 0) return null;
            var newDeposit = CreateTransaction<Deposit>();

            newDeposit.AccountReceiverId = receivingAccount.Id;
            newDeposit.CustomerReceiverId = receivingAccount.OwnerId;
            newDeposit.TimeOfTransaction = DateTime.Now;
            newDeposit.Amount = amount;
            receivingAccount.Balance += amount;

            MyBank?.DatabaseManager.WriteTransactionLog(newDeposit);

            return newDeposit;
        }

        public Withdrawal Withdraw(Account withdrawingAccount, decimal amount) {
            if (!HasEnoughFunds(withdrawingAccount, amount) || amount < 0) {
                return null;
            }

            var newTransfer = CreateTransaction<Withdrawal>();

            newTransfer.AccountReceiverId = withdrawingAccount.Id;
            newTransfer.CustomerReceiverId = withdrawingAccount.OwnerId;
            newTransfer.TimeOfTransaction = DateTime.Now;
            newTransfer.Amount = amount;
            withdrawingAccount.Balance -= amount;

            MyBank?.DatabaseManager.WriteTransactionLog(newTransfer);

            return newTransfer;
        }


        private static bool HasEnoughFunds(Account accountSending, decimal amount) {
            return accountSending.Balance - amount >= -accountSending.CreditRoof;
        }

        public IEnumerable<Transaction> GetTransactionsFromAccountId(int searchAccountId) {
            List<Transaction> returnTransactions = new List<Transaction>();
            foreach (Transaction transaction in Transactions) {
                if (transaction is IHasReceiver) {
                    IHasReceiver transactionWithReceive = (IHasReceiver)transaction;
                    if (transactionWithReceive.AccountReceiverId == searchAccountId) {
                        returnTransactions.Add(transaction);
                    }
                }
                if (transaction is IHasSender) {
                    IHasSender transactionWithSender = (IHasSender)transaction;
                    if (transactionWithSender.AccountSenderId == searchAccountId) {
                        returnTransactions.Add(transaction);
                    }
                }
            }

            return returnTransactions;
        }
    }
}
