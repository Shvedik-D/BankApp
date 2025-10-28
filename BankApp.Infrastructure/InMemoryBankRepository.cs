using BankApp.Core;
using BankApp.Core.Interfaces;

namespace BankApp.Infrastructure
{
    public class InMemoryBankRepository:IBankRepository
    {
     public IAccountRepository Accounts { get; }
     public ITransactionRepository Transactions { get; }

        public InMemoryBankRepository()
        {
            Accounts = new InMemoryAccount();
            Transactions = new InMemoryTransaction();
        }
    }
}
