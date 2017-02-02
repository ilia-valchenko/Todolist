using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces.DTO;
using NHibernate;
using ORM.Helpers;
using DAL.Mappers;
using ORM.Models;
using DAL.Interfaces.Repository.ModelRepository;

namespace DAL.Concrete
{
    public class TaskRepository : ITaskRepository
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

             using (ISession session = NhibernateHelper.OpenSession())
             {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Task deletingTask = session.Load<Task>(id);
                    session.Delete(deletingTask);
                    transaction.Commit();
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

        public IEnumerable<DalTask> GetTasksByDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
