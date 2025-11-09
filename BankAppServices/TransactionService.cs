using BankApp.Core;
using BankApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppServices
{
        public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactions;
        private readonly IAccountRepository _accounts;

        public TransactionService(ITransactionRepository transactions, IAccountRepository accounts)
        {
            _transactions = transactions;
            _accounts = accounts;
        }

        public IReadOnlyList<Transaction> GetStatement(Guid accountId)
        {
            var account = _accounts.GetById(accountId);
            if (account == null)
                throw new AccountNotFoundException(accountId);

            return _transactions.GetByAccountId(accountId);
        }
    }
}

