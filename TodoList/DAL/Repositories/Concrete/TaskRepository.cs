using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using NHibernate;
using DAL.Helpers;
using DAL.Repositories.Interfaces;
using NHibernate.Linq;

namespace DAL.Concrete
{
    public class TaskRepository : ITaskRepository
    {
        public void Create(DalTask task)
        {
            try
            {
                using (ISession session = NhibernateHelper.OpenSession())
                {
                    try
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(task);
                            transaction.Commit();
                        }
                    }
                    catch(TransactionException transactionException)
                    {
                        // ILogger
                        throw;
                    }
                }
            }
            catch(TransactionException transactionException)
            {
                // ILogger
                throw;
            }
            catch(SessionException sessionException)
            {
                // ILogger
                throw;
            }
            catch(Exception exception)
            {
                // ILogger
                throw;
            }
        }

        public void Update(DalTask task)
        {
            try
            {
                using (ISession session = NhibernateHelper.OpenSession())
                {
                    try
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Update(task);
                            transaction.Commit();
                        }
                    }
                    catch (TransactionException transactionException)
                    {
                        // ILogger
                        throw;
                    }
                }
            }
            catch(SessionException sessionException)
            {
                // ILogger
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (ISession session = NhibernateHelper.OpenSession())
                {
                    try
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            DalTask deletingTask = session.Load<DalTask>(id);
                            session.Delete(deletingTask);
                            transaction.Commit();
                        }
                    }
                    catch (TransactionException transactionException)
                    {
                        // write lo log
                        // ILogger.Write()
                        throw;
                    }
                }
            }
            catch(SessionException sessionException)
            {
                // write lo log
                // ILogger.Write()
            }
            catch(Exception exception)
            {
                // write lo log
                // ILogger.Write()
            }
        }

        public IEnumerable<DalTask> GetAll()
        {
            try
            {
                using (ISession session = NhibernateHelper.OpenSession())
                {
                    // session closed
                    //return session.Query<Task>().Select(t => t.ToDalTaks());
                    return session.CreateCriteria<DalTask>().List<DalTask>();
                }
            }
            catch(SessionException sessionException)
            {
                // ILogger
                throw;
            }
            catch(Exception exception)
            {
                // ILogger
                throw;
            }
        }

        public DalTask GetById(int id)
        {
            try
            {
                using (ISession session = NhibernateHelper.OpenSession())
                {
                    DalTask task = session.Get<DalTask>(id);
                    return task;
                }
            }
            catch(SessionException sessionException)
            {
                // log it
                throw;
            }
            catch(Exception exception)
            {
                // log it
                throw;
            }
        }
    }
}



