using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core
{
    public interface ITransactionRepository
    {
        void Add(Guid accountId, Transaction transaction);
        IReadOnlyList<Transaction> GetByAccountId(Guid accountId);
        IReadOnlyList<Transaction> GetAll();

    }
}