using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVega1.Persistance.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly VegaDbContext context;
        public UnitOfWork(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }

        public void DisposeResources()
        {
            context.Dispose();
        }
    }
}
