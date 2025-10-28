using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core
{
    public class SavingsAccount : Account
    {
        private const decimal InterestRate = 0.02m;

        public SavingsAccount(string ownerName) : base(ownerName) { }

        public override void Withdraw(decimal amount, string description = "")
        {
            if (amount <= 0) throw new InvalidAmountException();
            if (amount > Balance) throw new InsufficientFundsException();
            Deduct(amount, description);
        }

        public void ApplyInterest()
        {
            var interest = Balance * InterestRate;
            Deposit(interest, "Interest applied");
        }
    }
}
