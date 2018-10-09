using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _1809_BankApp;

namespace BankTests {
    [TestClass]
    public class Testing {
        [TestMethod]
        public void WithdrawAndTransfer_ValidAmount() {


            TransactionManager transactionManager = new TransactionManager();
            Account accountA = new Account() {
                Balance = 1000,
                CreditRoof = 2000
            };
            Account accountB = new Account() { Balance = 0 };


            decimal oldBalanceA = accountA.Balance;
            decimal oldBalanceB = accountB.Balance;
            transactionManager.Withdraw(accountA, 99999);
            Assert.AreEqual(oldBalanceA, accountA.Balance);

            oldBalanceA = accountA.Balance;
            oldBalanceB = accountB.Balance;
            transactionManager.Transfer(accountA, accountB, 99999);
            Assert.AreEqual(oldBalanceA, accountA.Balance);
            Assert.AreEqual(oldBalanceB, accountB.Balance);

            oldBalanceA = accountA.Balance;
            oldBalanceB = accountB.Balance;
            decimal amountToWithdraw = accountA.Balance + accountA.CreditRoof / 2;
            transactionManager.Withdraw(accountA, amountToWithdraw);
            Assert.IsTrue(accountA.Balance < 0);
            Assert.IsTrue(accountA.Balance > -accountA.CreditRoof);
        }

        [TestMethod]
        public void WithdrawDepositAndTransfer_NegativeAmount() {
            TransactionManager transactionManager = new TransactionManager();
            decimal oldBalanceA = 1000;
            decimal oldBalanceB = 0;
            Account accountA = new Account() { Balance = oldBalanceA };
            Account accountB = new Account() { Balance = oldBalanceB };

            transactionManager.Withdraw(accountA, -100);
            Assert.AreEqual(oldBalanceA, accountA.Balance);

            transactionManager.Deposit(accountA, -100);
            Assert.AreEqual(oldBalanceA, accountA.Balance);

            transactionManager.Transfer(accountA, accountB, -100);
            Assert.AreEqual(oldBalanceA, accountA.Balance);
            Assert.AreEqual(oldBalanceB, accountB.Balance);
        }

        [TestMethod]
        public void DebitInterests() {
            Account debitAccount = new Account() {
                Balance = 100,
                DebitInterestYearly = .02
            };

            for (int i = 0; i < 365; i++) {
                InterestApplication.ApplyDailyInterest(debitAccount);
            }
            Assert.AreEqual(102, Math.Round(debitAccount.Balance, 2));
        }

        [TestMethod]
        public void CreditInterest() {
            Account debitAccount = new Account() {
                Balance = -100,
                CreditInterestYearly = .02
            };

            for (int i = 0; i < 365; i++) {
                InterestApplication.ApplyDailyInterest(debitAccount);
            }
            Assert.AreEqual(-102, Math.Round(debitAccount.Balance, 2));
        }


    }
}
