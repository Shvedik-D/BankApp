using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Interfaces
{
    public interface ITransferService
    {
        void Transfer(Guid fromAccountId, Guid toAccountId,decimal amount);
        
    }
}
