using System.Collections.Generic;
using DAL.Entities;
using NHibernate;
using DAL.Repositories.Interfaces;
using NHibernate.Linq;
using System.Linq;

namespace DAL.Repositories.DatabaseRepositories
{
    public sealed class TaskRepository : ITaskRepository
    {
        private readonly ISessionFactory sessionFactory;

        public TaskRepository(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        #region CRUD
        public int Create(TaskEntity task)
        {
            object resultOfSaving;

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    resultOfSaving = session.Save(task);
                    transaction.Commit();
                }
            }

            int generatedId = (int)resultOfSaving;
            return generatedId;
        }

        public void Update(TaskEntity task)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(task);
                    transaction.Commit();
                }
            }
        }

        public void Delete(TaskEntity task)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(task);
                    transaction.Commit();
                }
            }
        }
        #endregion

        #region Get operations
        public IEnumerable<TaskEntity> GetAll()
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var tasks = session.Query<TaskEntity>();
                return tasks.ToList();
            }
        }

        public TaskEntity GetById(int id)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                TaskEntity task = session.Get<TaskEntity>(id);
                return task;
            }
        } 
        #endregion
    }
}




