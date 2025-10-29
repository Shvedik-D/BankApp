using BankApp.Core;
 
namespace BankApp.Infrastructure
{
    public class InMemoryAccount : IAccountRepository
    {
        private readonly Dictionary<Guid, Account> _accounts = new();
        public Account? GetById(Guid id) => _accounts.TryGetValue(id, out var account) ? account : null;
        public void Save (Account account)=> _accounts[account.Id]= account;

        public IEnumerable<Account> GetAll() => _accounts.Values;

    }
}
