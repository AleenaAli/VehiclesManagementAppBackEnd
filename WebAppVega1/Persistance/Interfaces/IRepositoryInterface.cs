using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppVega1.Models;

namespace WebAppVega1.Persistance.Interfaces
{
    public interface IRepositoryInterface<TEntity> where TEntity : class
    {
        TEntity[] GetList();
        Task<Vehicle> GetVehicle(int id);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void UpdateEntity(TEntity entity);

    }
}
