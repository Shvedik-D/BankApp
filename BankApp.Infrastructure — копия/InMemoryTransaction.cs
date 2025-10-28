using BankApp.Core;

namespace BankApp.Infrastructure
{
    public class InMemoryTransaction: ITransactionRepository
    {
        private readonly Dictionary<Guid, List<Transaction>> _store = new();
        public void Add(Guid accountId, Transaction transaction)
        {
            if (!_store.ContainsKey(accountId))
                _store[accountId] = new List<Transaction>();
            _store[accountId].Add(transaction);
        }
        public IReadOnlyList<Transaction> GetByAccountId(Guid AccountId)
        {
            return _store.TryGetValue(AccountId, out var list)
                ? list.AsReadOnly()
                : new List<Transaction>().AsReadOnly();

        }
        public IReadOnlyList<Transaction> GetAll()
        {
            return _store.Values.SelectMany(x => x).ToList().AsReadOnly();
        }
    }
}
