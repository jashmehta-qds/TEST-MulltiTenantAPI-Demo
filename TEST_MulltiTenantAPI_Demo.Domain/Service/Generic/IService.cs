﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TEST_MulltiTenantAPI_Demo.Domain.Service
{
    public interface IService<Tv, Te>
    {
        IEnumerable<Tv> GetAll();
        int Add(Tv obj);
        int Update(Tv obj);
        int Remove(int id);
        Tv GetOne(int id);
        IEnumerable<Tv> Get(Expression<Func<Te, bool>> predicate);
    }
}
