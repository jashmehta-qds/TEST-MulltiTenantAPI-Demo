using TEST_MulltiTenantAPI_Demo.Entity.Context;
using TEST_MulltiTenantAPI_Demo.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TEST_MulltiTenantAPI_Demo.Entity.UnitofWork
{

    public interface IUnitOfWork : IDisposable
    {
        int Save();
        Task<int> SaveAsync();


    }

    public interface IMasterUnitOfWork: IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, IMasterEntity;

        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : BaseEntity, IMasterEntity;

        MasterDbContext Context { get; }
    }

    public interface ISlaveUnitOfWork: IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, ISlaveEntity;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : BaseEntity, ISlaveEntity;

        SlaveDbContext Context { get; }
    }

    public class MasterUnitOfWork : IMasterUnitOfWork
    {
        public MasterDbContext Context { get; }

        private Dictionary<Type, object> _repositories;
        private Dictionary<Type, object> _repositoriesAsync;
        private bool _disposed;
     

        public MasterUnitOfWork(MasterDbContext context)
        {
            Context = context;
            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, IMasterEntity
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new MasterRepository<TEntity>(this);

            return (IRepository<TEntity>)_repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : BaseEntity, IMasterEntity
        {
            if (_repositoriesAsync == null) _repositoriesAsync = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositoriesAsync.ContainsKey(type)) _repositoriesAsync[type] = new MasterRepositoryAsync<TEntity>(this);
            return (IRepositoryAsync<TEntity>)_repositoriesAsync[type];
        }
        public int Save()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }
        public async Task<int> SaveAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }
    }

    public class SlaveUnitOfWork : ISlaveUnitOfWork
    {
        public SlaveDbContext Context { get; }
        private Dictionary<Type, object> _repositoriesAsync;
        private Dictionary<Type, object> _repositories;
        private bool _disposed;

        public SlaveUnitOfWork(SlaveDbContext context)
        {
            Context = context;
            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, ISlaveEntity
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new SlaveRepository<TEntity>(this);
            return (IRepository<TEntity>)_repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : BaseEntity, ISlaveEntity
        {
            if (_repositoriesAsync == null) _repositoriesAsync = new Dictionary<Type, object>(); 
            var type = typeof(TEntity);
            if (!_repositoriesAsync.ContainsKey(type)) _repositoriesAsync[type] = new SlaveRepositoryAsync<TEntity>(this); 
            return (IRepositoryAsync<TEntity>)_repositoriesAsync[type];
        }


        public int Save()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }
        public async Task<int> SaveAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }
    }

}
