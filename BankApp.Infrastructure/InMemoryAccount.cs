using BankApp.Core;
 
namespace BankApp.Infrastructure
{
    public class InMemoryAccount : IAccountRepository
    {
        private readonly Dictionary<Guid, Account> _store = new();
        public Account? GetById(Guid id) => _store.TryGetValue(id, out var account) ? account : null;
        public void Save (Account account)=> _store[account.Id]= account;

        public IEnumerable<Account> GetAll() => _store.Values;

    }
}
