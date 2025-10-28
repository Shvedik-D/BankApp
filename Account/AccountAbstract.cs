using System.Transactions;

public abstract class Account
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string OwnerName { get; private set; }
    public decimal Balance { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public List<Transaction> Transactions { get; private set; } = new();

    protected Account(string ownerName)
    {
        OwnerName = ownerName;
    }

    public void Deposit(decimal amount, string description = "")
    {
        if (amount <= 0) throw new InvalidAmountException();
        Balance += amount;
        AddTransaction(TransactionType.Deposit, amount, description);
    }

    protected void Deduct(decimal amount, string description = "")
    {
        Balance -= amount;
        AddTransaction(TransactionType.Withdraw, amount, description);
    }

    private void AddTransaction(TransactionType type, decimal amount, string description)
    {
        Transactions.Add(new Transaction(type, amount, Balance, description));
    }

    public abstract void Withdraw(decimal amount, string description = "");
}
