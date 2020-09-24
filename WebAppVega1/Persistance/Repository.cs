using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppVega1.Models;
using WebAppVega1.Persistance.Interfaces;

namespace WebAppVega1.Persistance
{
    public class Repository<TEntity> : IRepositoryInterface<TEntity> where TEntity: class
    {
        protected readonly VegaDbContext context;

        protected readonly IUnitOfWork UnitOfWork;

        public Repository(VegaDbContext context, IUnitOfWork unitOfWork)
        {
            this.context = context;
            UnitOfWork = unitOfWork;
        }
        public TEntity[] GetList()
        {
            return context.Set<TEntity>().AsEnumerable().ToArray();
        }
        public async Task<Vehicle> GetVehicle(int id)
        {
            Vehicle vehicle= await context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
            return vehicle;
        }

        //public async Task<Vehicle> GetVehicle2(int id)
        //{
        //    return await context.Vehicles.Include(v => v.Features)
        //        .Include(v => v.Model)
        //        .ThenInclude(v => v.MakeID);
        //}
        public void UpdateEntity(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            UnitOfWork.Complete();
        }

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            UnitOfWork.Complete();
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            UnitOfWork.Complete();
        }
    }
}
