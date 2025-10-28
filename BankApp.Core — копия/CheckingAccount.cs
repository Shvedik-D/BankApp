using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core
{
    public class CheckingAccount : Account
    {
        private const decimal OverdraftLimit = -500m;

        public CheckingAccount(string ownerName) : base(ownerName) { }

        public override void Withdraw(decimal amount, string description = "")
        {
            if (amount <= 0) throw new InvalidAmountException();
            if (Balance - amount < OverdraftLimit) throw new InsufficientFundsException();
            Deduct(amount, description);

        }
    }
}
