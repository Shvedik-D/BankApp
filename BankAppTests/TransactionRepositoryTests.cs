using BankApp.Core;
using BankApp.Infrastructure;

namespace BankAppTests;

public class TransactionRepositoryTests
{
    private readonly ITransactionRepository _repo = new InMemoryTransaction();

    [Fact]
    public void Add_ShouldStoreTransaction()
    {
        var accountId = Guid.NewGuid();
        var tx = new Transaction(TransactionType.Deposit, 100m, 100m, "Initial deposit");

        _repo.Add(accountId, tx);
        var result = _repo.GetByAccountId(accountId);

        Assert.Single(result);
        Assert.Equal(tx, result[0]);
    }

    [Fact]
    public void GetAll_ShouldReturnAllTransactions()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();

        _repo.Add(id1, new Transaction(TransactionType.Deposit, 100m, 100m, "A"));
        _repo.Add(id2, new Transaction(TransactionType.Withdraw, 50m, 50m, "B"));

        var all = _repo.GetAll();
        Assert.Equal(2, all.Count);
    }
}

