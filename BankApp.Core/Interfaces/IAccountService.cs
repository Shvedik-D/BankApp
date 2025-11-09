using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core
{
    public interface IAccountService
    {
        Guid CreateAccount(AccountType type, string ownerName, decimal initialDeposit);
        void Deposit(Guid accountId, decimal amount);
        void Withdraw(Guid accountId, decimal amount);
        
    }
}
