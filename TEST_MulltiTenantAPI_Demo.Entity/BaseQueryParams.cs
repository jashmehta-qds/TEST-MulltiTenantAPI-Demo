using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TEST_MulltiTenantAPI_Demo.Entity
{
    public class BaseQueryParams
    {
        public string SortOrder { get; set; }
        public string SortField { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
