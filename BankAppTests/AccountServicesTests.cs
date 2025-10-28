using BankApp.Core;
using BankAppServices;
using Xunit;

public class AccountServiceTests
        {
            private readonly IAccountService _service = new AccountService();

            [Fact]
            public void CreateAccount_ShouldCreateCheckingAccount_WithInitialDeposit()
            {
                var id = _service.CreateAccount(AccountType.Checking, "Dasha", 1000m);
                var statement = _service.GetStatement(id);

                Assert.Single(statement);
                Assert.Equal(TransactionType.Deposit, statement[0].Type);
                Assert.Equal(1000m, statement[0].BalanceAfter);
            }

            [Fact]
            public void Deposit_ShouldIncreaseBalance()
            {
                var id = _service.CreateAccount(AccountType.Savings, "Dasha", 500m);
                _service.Deposit(id, 200m);

                var statement = _service.GetStatement(id);
                Assert.Equal(2, statement.Count);
                Assert.Equal(700m, statement[^1].BalanceAfter);
            }

            [Fact]
            public void Withdraw_ShouldDecreaseBalance()
            {
                var id = _service.CreateAccount(AccountType.Savings, "Dasha", 500m);
                _service.Withdraw(id, 300m);

                var statement = _service.GetStatement(id);
                Assert.Equal(2, statement.Count);
                Assert.Equal(200m, statement[^1].BalanceAfter);
            }

            [Fact]
            public void Transfer_ShouldMoveFundsBetweenAccounts()
            {
                var fromId = _service.CreateAccount(AccountType.Checking, "Sender", 1000m);
                var toId = _service.CreateAccount(AccountType.Savings, "Receiver", 100m);

                _service.Transfer(fromId, toId, 300m);

                var fromStatement = _service.GetStatement(fromId);
                var toStatement = _service.GetStatement(toId);

                Assert.Equal(2, fromStatement.Count);
                Assert.Equal(2, toStatement.Count);
                Assert.Equal(700m, fromStatement[^1].BalanceAfter);
                Assert.Equal(400m, toStatement[^1].BalanceAfter);
            }

            [Fact]
            public void Withdraw_ShouldThrow_WhenAmountIsNegative()
            {
                var id = _service.CreateAccount(AccountType.Savings, "Dasha", 500m);
                Assert.Throws<InvalidAmountException>(() => _service.Withdraw(id, -100m));
            }

            [Fact]
            public void Withdraw_ShouldThrow_WhenInsufficientFunds()
            {
                var id = _service.CreateAccount(AccountType.Savings, "Dasha", 100m);
                Assert.Throws<InsufficientFundsException>(() => _service.Withdraw(id, 200m));
            }

            [Fact]
            public void Deposit_ShouldThrow_WhenAmountIsZero()
            {
                var id = _service.CreateAccount(AccountType.Checking, "Dasha", 100m);
                Assert.Throws<InvalidAmountException>(() => _service.Deposit(id, 0m));
            }

            [Fact]
            public void GetStatement_ShouldThrow_WhenAccountNotFound()
            {
                var invalidId = Guid.NewGuid();
                var ex = Assert.Throws<AccountNotFoundException>(() => _service.GetStatement(invalidId));
                Assert.Equal(invalidId, ex.AccountId);
            }
        }

  