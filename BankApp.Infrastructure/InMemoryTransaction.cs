using BankApp.Core;

namespace BankApp.Infrastructure
{
    public class InMemoryTransaction: ITransactionRepository
    {
        private readonly Dictionary<Guid, List<Transaction>> _transactions = new();
        public void Add(Guid accountId, Transaction transaction)
        {
            if (!_transactions.ContainsKey(accountId))
                _transactions[accountId] = new List<Transaction>();
            _transactions[accountId].Add(transaction);
        }
        public IReadOnlyList<Transaction> GetByAccountId(Guid AccountId)
        {
            return _transactions.TryGetValue(AccountId, out var list)
                ? list.AsReadOnly()
                : new List<Transaction>().AsReadOnly();

        }
        public IReadOnlyList<Transaction> GetAll()
        {
            return _transactions.Values.SelectMany(x => x).ToList().AsReadOnly();
        }
    }
}
