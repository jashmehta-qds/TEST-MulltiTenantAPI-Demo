using TEST_MulltiTenantAPI_Demo.Entity.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_MulltiTenantAPI_Demo.Entity.Repository
{
    class UserRepository : MasterRepository<User>
    {
        public UserRepository(IMasterUnitOfWork unitOfWork) : base(unitOfWork) { 
        
        }


    }
}
