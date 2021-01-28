using AutoMapper;
using TEST_MulltiTenantAPI_Demo.Domain.Service.Generic;
using TEST_MulltiTenantAPI_Demo.Entity;
using TEST_MulltiTenantAPI_Demo.Entity.UnitofWork;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace TEST_MulltiTenantAPI_Demo.Domain.Service
{
    public class UserService<Tv, Te> : MasterGenericService<Tv, Te>
                                                where Tv : UserViewModel
                                                where Te : User
    {
        //DI must be implemented in specific service as well beside GenericService constructor
        public UserService(IMasterUnitOfWork unitOfWork, IMapper mapper)
        {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
        }

        //add here any custom service method or override generic service method
        public bool DoNothing()
        {
            return true;
        }

        /// <summary>
        /// 
        /// These service calls are examples of stored procedure use in Apincore REAT API serice
        /// READbyStoredProcedure(sql, parameters)
        /// CUDbyStoredProcedure(sql, parameters)
        /// 
        /// </summary>
        /// 

        //stored procedure READ
        //note:sp params must be in the same order like in sp
        public IEnumerable<UserViewModel> GetUsersByName(string firstName, string lastName)
        {
            var parameters = new[] {
                new SqlParameter("@FirstName", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = firstName },
                new SqlParameter("@LastName", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = lastName }
                };
            string sql = "EXEC [dbo].[prGetUserByFirstandLastName] @FirstName, @LastName";

            var users = _unitOfWork.GetRepository<User>().READbyStoredProcedure(sql, parameters);
            return _mapper.Map<IEnumerable<UserViewModel>>(source: users);
        }


        //stored procedure CREATE UPDATE DELETE
        //note:sp params must be in the same order like in sp
        public int UpdateEmailByUsername(string username, string email)
        {
            var parameters = new[] {
                new SqlParameter("@UserName", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = username },
                new SqlParameter("@Email", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = email }
                };
            string sql = "EXEC [dbo].[prUpdateEmailByUsername] @UserName, @Email";

            int records = _unitOfWork.GetRepository<User>().CUDbyStoredProcedure(sql, parameters);
            return records;
        }


    }

}
