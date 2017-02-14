using System.Collections.Generic;
using DAL.Entities;
using NHibernate;
using DAL.Repositories.Interfaces;
using NHibernate.Linq;
using System.Linq;

namespace DAL.Concrete
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ISessionFactory sessionFactory;

        public TaskRepository(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public int Create(DalTask task)
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

        public void Update(DalTask task)
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

        public void Delete(DalTask task)
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

        public IEnumerable<DalTask> GetAll()
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var tasks = session.Query<DalTask>();
                return tasks.ToList();
            }
        }

        public DalTask GetById(int id)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                DalTask task = session.Get<DalTask>(id);
                return task;
            }
        }
    }
}




