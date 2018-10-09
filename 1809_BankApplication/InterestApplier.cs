using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1809_BankApp {
    public static class InterestApplier {
        public static void ApplyDailyInterest(Account account) {
            double Multiplier = account.Balance > 0 ? account.DebitInterestYearly + 1 : account.CreditInterestYearly + 1;
            account.Balance = account.Balance * (decimal)Math.Pow((Multiplier), 1 / 365d);
        }
    }
}
