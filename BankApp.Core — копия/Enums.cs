using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core
{
    public enum TransactionType
    {
        Deposit,
        Withdraw,
        Transfer
    }

    public enum AccountType
    {
        Checking,
        Savings,
        Business
    }
}

