using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class TransactionManager {
        public List<ITransaction> Transactions { get; set; }

        public ITransaction CreateTransaction() {
            Transfer newTransfer = new Transfer();
            Transactions.Add(newTransfer);
            return newTransfer;
        }

        public bool TryTransfer(Account sendingAccount, Account receivingAccount, decimal amount, ref Transfer newTransfer) {
            if (!HasEnoughFunds(sendingAccount, amount)) return false;

            newTransfer = CreateTransaction();
            newTransfer.AccountSenderId = sendingAccount.Id;
            newTransfer.AccountReceiverId = receivingAccount.Id;
            newTransfer.CustomerSenderId = sendingAccount.OwnerId;
            newTransfer.CustomerReceiverId = receivingAccount.OwnerId;
            newTransfer.TimeOfTransaction = DateTime.Now;
            receivingAccount.Balance += amount;
            sendingAccount.Balance -= amount;

            Program.DatabaseManager.AddTransaction(newTransfer);
            Program.DatabaseManager.UpdateAccount(new List<Account>() { sendingAccount, receivingAccount });

            return true;
        }

        public bool TryDeposit(Account receivingAccount, decimal amount, ref Deposit newDeposit) {
            newDeposit = new Deposit();

            return true;
        }

        private static bool HasEnoughFunds(Account accountSending, decimal amount) {
            return accountSending.Balance - amount >= accountSending.CreditRoof;
        }
    }
}
