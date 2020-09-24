using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVega1.Persistance.Interfaces
{
    public interface IUnitOfWork
    {
        Task Complete();
        void DisposeResources();
    }
}
