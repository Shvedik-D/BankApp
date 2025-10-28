using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base("Insufficient funds to complete the operation.") { }
    }

    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() : base("The amount must be positive.") { }
    }
    public class AccountNotFoundException : Exception
    {
        public Guid AccountId { get; }

        public AccountNotFoundException(Guid accountId)
            : base($"Account with ID {accountId} was not found.")
        {
            AccountId = accountId;
        }
    }


   
    

}