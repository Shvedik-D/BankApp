using BankApp.Core;
using BankAppServices;
using Xunit;

namespace BankAppTests
{
    public class TransferServiceTests
    {
        private readonly IAccountRepository _accounts = new InMemoryAccountRepository();
        private readonly ITransactionRepository _transactions = new InMemoryTransactionRepository();
        private readonly TransferService _service;

        public TransferServiceTests()
        {
            _service = new TransferService(_accounts, _transactions);
        }

        [Fact]
        public void Transfer_ShouldMoveFundsBetweenAccounts()
        {
            var fromId = new AccountService().CreateAccount(AccountType.Checking, "Alice", 1000);
            var toId = new AccountService().CreateAccount(AccountType.Savings, "Bob", 500);

            _service.Transfer(fromId, toId, 300);

            Assert.Equal(700, _accounts.GetById(fromId).Balance);
            Assert.Equal(800, _accounts.GetById(toId).Balance);
        }

        [Fact]
        public void Transfer_ShouldThrowIfInsufficientFunds()
        {
            var fromId = new AccountService().CreateAccount(AccountType.Checking, "Alice", 100);
            var toId = new AccountService().CreateAccount(AccountType.Savings, "Bob", 500);

            Assert.Throws<InsufficientFundsException>(() => _service.Transfer(fromId, toId, 200));
        }
    }
}
