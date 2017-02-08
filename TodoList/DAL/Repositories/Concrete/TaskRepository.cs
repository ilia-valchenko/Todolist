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
                    string queryString = string.Format("update {0} as task set task.Title = :newTitle, task.Description = :newDescription where task.Id = :id", typeof(DalTask));
                    IQuery query = session.CreateQuery(queryString).SetParameter("newTitle", task.Title).SetParameter("newDescription", task.Description).SetParameter("id", task.Id);
                    int resultOfExcutionUpdate = query.ExecuteUpdate();
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
                    string queryString = string.Format("delete {0} where id = :id", typeof(DalTask));
                    IQuery query = session.CreateQuery(queryString).SetParameter("id", id);
                    int resultOfExcutionUpdate = query.ExecuteUpdate();
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
                ICriteria criteria = session.CreateCriteria<DalTask>();
                return criteria.List<DalTask>();
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




