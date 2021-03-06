﻿using System.Collections.Generic;

namespace DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
    }
}
