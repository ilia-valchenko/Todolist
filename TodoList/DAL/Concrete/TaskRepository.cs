﻿using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using NHibernate;
using DAL.Helpers;
using DAL.Mappers;
using ORM.Models;

namespace DAL.Concrete
{
    public class TaskRepository : IRepository<DalTask>
    {
        public void Create(DalTask entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (ISession session = NhibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity.ToOrmTask());
                    transaction.Commit();
                }
            }
        }

        public void Update(DalTask entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (ISession session = NhibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(entity);
                    transaction.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("The Id of a deleting element can't be less then zero.");

            DalTask deletingTask = GetById(id);

            if(deletingTask != null)
            {
                using (ISession session = NhibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(deletingTask);
                        transaction.Commit();
                    }
                }
            }
        }

        public IEnumerable<DalTask> GetAll()
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                return session.CreateCriteria<Task>().List<Task>().Select(t => t.ToDalTaks());
            }
        }

        public DalTask GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("The Id of a seeking element can't be less then zero.");

            using (ISession session = NhibernateHelper.OpenSession())
            {
                return session.Get<Task>(id).ToDalTaks();
            }
        }
    }
}
