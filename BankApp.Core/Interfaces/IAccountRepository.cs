using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core
{
    public interface IAccountRepository
    {
        Account GetById(Guid id);
        void Save(Account account);
        IEnumerable<Account> GetAll();
    }
}
