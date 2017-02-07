using System.Collections.Generic;
using DAL.Entities;
using NHibernate;
using DAL.Repositories.Interfaces;
using NHibernate.Linq;

namespace DAL.Concrete
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ISessionFactory sessionFactory;

        public TaskRepository(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public void Create(DalTask task)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(task);
                    transaction.Commit();
                }
            }
        }

        public void Update(DalTask task)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    // test
                    DalTask oldTask = session.Load<DalTask>(task.Id);
                    oldTask.Title = task.Title;
                    oldTask.Description = task.Description;

                    //session.Update(task);
                    session.Update(oldTask);

                    transaction.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    DalTask deletingTask = session.Load<DalTask>(id);
                    session.Delete(deletingTask);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<DalTask> GetAll()
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                // session closed
                //return session.Query<Task>().Select(t => t.ToDalTaks());
                IEnumerable<DalTask> tasks = session.CreateCriteria<DalTask>().List<DalTask>();
                return tasks;
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




