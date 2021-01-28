using TEST_MulltiTenantAPI_Demo.Entity.UnitofWork;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TEST_MulltiTenantAPI_Demo.Entity.Repository
{
    class MasterRepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity, IMasterEntity
    {

        private readonly IMasterUnitOfWork _unitOfWork;
        public MasterRepositoryAsync(IMasterUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }
        public async Task<IEnumerable<T>> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _unitOfWork.Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _unitOfWork.Context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }
        public async Task Insert(T entity)
        {
            if (entity != null)
                await _unitOfWork.Context.Set<T>().AddAsync(entity);
        }
        public async Task Update(object id, T entity)
        {
            if (entity != null)
            {
                //T entitytoUpdate = await _unitOfWork.Context.Set<T>().FindAsync(id);
                //if (entitytoUpdate != null)
                //	_unitOfWork.Context.Entry(entitytoUpdate).CurrentValues.SetValues(entity);
                _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
            }
        }
        public async Task Delete(object id)
        {
            T entity = await _unitOfWork.Context.Set<T>().FindAsync(id);
            Delete(entity);
        }
        public void Delete(T entity)
        {
            if (entity != null) _unitOfWork.Context.Set<T>().Remove(entity);
        }

        public Task<List<T>> READbyStoredProcedure(string sql, SqlParameter[] parameters)
        {
            return _unitOfWork.Context.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
        }

        public Task<int> CUDbyStoredProcedure(string sql, SqlParameter[] parameters)
        {
            return _unitOfWork.Context.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}
