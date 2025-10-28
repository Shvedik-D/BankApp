using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core
{
    public class BusinessAccount : Account
    {
        private readonly decimal WithdrawalFee = 5m;

        public BusinessAccount(string ownerName) : base(ownerName) { }

        public override void Withdraw(decimal amount, string description = "")
        {
            var totalAmount = amount + WithdrawalFee;
            if (amount <= 0) throw new InvalidAmountException();
            if (totalAmount > Balance) throw new InsufficientFundsException();
            Deduct(totalAmount, $"{description} (Fee: {WithdrawalFee})");
        }
    }
}
