using BankApp.Core;
using BankAppServices;
using BankApp.Infrastructure;
using Xunit;

namespace BankAppTests
{
   public class TransactionServiceTests
    {
        private readonly IAccountRepository _accounts = new InMemoryAccount();
        private readonly ITransactionRepository _transactions = new InMemoryTransaction();
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            _service = new TransactionService(_transactions, _accounts);
        }

        [Fact]
        public void GetStatement_ShouldReturnTransactions()
        {
            var accountId = new AccountService().CreateAccount(AccountType.Checking, "Dasha", 1000);
            _transactions.Add(accountId, new Transaction(TransactionType.Deposit, 1000, 1000, "Initial"));

            var statement = _service.GetStatement(accountId);

            Assert.Single(statement);
            Assert.Equal(TransactionType.Deposit, statement[0].Type);
        }

        [Fact]
        public void GetStatement_ShouldThrowIfAccountNotFound()
        {
            var unknownId = Guid.NewGuid();
            Assert.Throws<AccountNotFoundException>(() => _service.GetStatement(unknownId));
        }
    }
}
