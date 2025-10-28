
using BankApp.Core;
using System.Security.Principal;

namespace BankAppServices
{
    public class AccountService :IAccountService
    {
        private readonly Dictionary<Guid, Account> _accounts = new();

        public Guid CreateAccount(AccountType type, string ownerName, decimal initialDeposit)
        {
            Account account = type switch
            {
                AccountType.Checking => new CheckingAccount(ownerName),
                AccountType.Savings => new SavingsAccount(ownerName),
                AccountType.Business => new BusinessAccount(ownerName),
                _ => throw new ArgumentException("Invalid account type")
            };

            account.Deposit(initialDeposit);
            _accounts[account.Id] = account;
            return account.Id;
        }

        public void Deposit(Guid accountId, decimal amount)
        {
            var account = GetAccount(accountId);
            account.Deposit(amount);
        }

        public void Withdraw(Guid accountId, decimal amount)
        {
            var account = GetAccount(accountId);
            account.Withdraw(amount);
        }

        public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = GetAccount(fromAccountId);
            var to = GetAccount(toAccountId);

            from.Withdraw(amount, $"Transfer to {to.OwnerName}");
            to.Deposit(amount, $"Transfer from {from.OwnerName}");
        }

        public IReadOnlyList<Transaction> GetStatement(Guid accountId)
        {
            var account = GetAccount(accountId);
            return account.Transactions.AsReadOnly();
        }

        private Account GetAccount(Guid id)
        {
            if (!_accounts.TryGetValue(id, out var account))
                throw new AccountNotFoundException(id);
            return account;
        }
    }
}