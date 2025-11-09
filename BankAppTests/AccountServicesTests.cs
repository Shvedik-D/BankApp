using BankApp.Core;
using BankAppServices;
using Xunit;

public class AccountServiceTests
{
    private readonly IAccountRepository _repo = new InMemoryAccountRepository();
    private readonly AccountService _service;

    public AccountServiceTests()
    {
        _service = new AccountService();
    }

    [Fact]
    public void CreateAccount_ShouldCreateCheckingAccount()
    {
        var id = _service.CreateAccount(AccountType.Checking, "Dasha", 1000);
        var account = _repo.GetById(id);

        Assert.NotNull(account);
        Assert.Equal("Dasha", account.OwnerName);
        Assert.Equal(1000, account.Balance);
        Assert.IsType<CheckingAccount>(account);
    }

    [Fact]
    public void Deposit_ShouldIncreaseBalance()
    {
        var id = _service.CreateAccount(AccountType.Savings, "Dasha", 500);
        _service.Deposit(id, 200);

        var account = _repo.GetById(id);
        Assert.Equal(700, account.Balance);
    }

    [Fact]
    public void Withdraw_ShouldDecreaseBalance()
    {
        var id = _service.CreateAccount(AccountType.Business, "Dasha", 1000);
        _service.Withdraw(id, 300);

        var account = _repo.GetById(id);
        Assert.Equal(700, account.Balance);
    }
}