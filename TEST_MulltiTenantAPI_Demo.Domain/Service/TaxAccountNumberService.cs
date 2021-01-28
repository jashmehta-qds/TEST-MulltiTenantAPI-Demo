using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using TEST_MulltiTenantAPI_Demo.Domain.Service.Generic;
using TEST_MulltiTenantAPI_Demo.Entity;
using TEST_MulltiTenantAPI_Demo.Entity.UnitofWork;

namespace TEST_MulltiTenantAPI_Demo.Domain.Service
{

    /// <summary>
    ///
    /// A TaxAccountNumber service
    ///       
    /// </summary>

    public class TaxAccountNumberServiceAsync<Tv, Te> : SlaveGenericServiceAsync<Tv, Te>
                                        where Tv : TaxAccountNumberViewModel
                                        where Te : TaxAccountNumber
    {
        //DI must be implemented in specific service as well beside GenericService constructor

        //add here any custom service method or override generic service method
        public TaxAccountNumberServiceAsync(ISlaveUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;
        }


    }

}