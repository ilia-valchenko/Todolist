using System.Collections.Generic;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);

        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
    }
}
