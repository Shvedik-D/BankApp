using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core
{
    public record Transaction(TransactionType Type,decimal Amount,decimal BalanceAfter,string Description)
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime Date { get; init; } = DateTime.UtcNow;
    }
}
