using BankApp.Core;
using BankApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppServices
{
        public class TransferService : ITransferService
    {
        private readonly IAccountRepository _accounts;
        private readonly ITransactionRepository _transactions;

        public TransferService(IAccountRepository accounts, ITransactionRepository transactions)
        {
            _accounts = accounts;
            _transactions = transactions;
        }

        public void Transfer(Guid fromId, Guid toId, decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException();

            var from = _accounts.GetById(fromId) ?? throw new AccountNotFoundException(fromId);
            var to = _accounts.GetById(toId) ?? throw new AccountNotFoundException(toId);

            if (from.Balance < amount)
                throw new InsufficientFundsException();

            from.Withdraw(amount, $"Transfer to {to.OwnerName}");
            to.Deposit(amount, $"Transfer from {from.OwnerName}");

            _accounts.Save(from);
            _accounts.Save(to);

            _transactions.Add(from.Id, new Transaction(TransactionType.Transfer, -amount, from.Balance, $"To {to.OwnerName}"));
            _transactions.Add(to.Id, new Transaction(TransactionType.Transfer, amount, to.Balance, $"From {from.OwnerName}"));
        }
    }
}
