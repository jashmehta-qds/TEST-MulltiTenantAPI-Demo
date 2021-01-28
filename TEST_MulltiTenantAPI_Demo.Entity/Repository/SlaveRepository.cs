using TEST_MulltiTenantAPI_Demo.Entity.UnitofWork;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TEST_MulltiTenantAPI_Demo.Entity.Repository
{
    class SlaveRepository<T> : IRepository<T> where T : BaseEntity, ISlaveEntity
    {

        private readonly ISlaveUnitOfWork _unitOfWork;
        public SlaveRepository(ISlaveUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int CUDbyStoredProcedure(string sql, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _unitOfWork.Context.Set<T>();
        }
        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork.Context.Set<T>().Where(predicate).AsEnumerable<T>();
        }
        public T GetOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork.Context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> READbyStoredProcedure(string sql, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Update(object id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
