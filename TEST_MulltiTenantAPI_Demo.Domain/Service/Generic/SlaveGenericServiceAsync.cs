﻿using AutoMapper;
using TEST_MulltiTenantAPI_Demo.Entity;
using TEST_MulltiTenantAPI_Demo.Entity.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TEST_MulltiTenantAPI_Demo.Domain.Service.Generic
{
    public class SlaveGenericServiceAsync<Tv, Te> : IServiceAsync<Tv, Te> where Tv : BaseDomain
                                  where Te : BaseEntity, ISlaveEntity
    {
        protected ISlaveUnitOfWork _unitOfWork;
        protected IMapper _mapper;
        public SlaveGenericServiceAsync(ISlaveUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public virtual async Task<IEnumerable<Tv>> GetAll()
        {
            var entities = await _unitOfWork.GetRepositoryAsync<Te>()
                .GetAll();
            return _mapper.Map<IEnumerable<Tv>>(source: entities);
        }

        public virtual async Task<Tv> GetOne(int id)
        {
            var entity = await _unitOfWork.GetRepositoryAsync<Te>()
                .GetOne(predicate: x => x.Id == id);
            return _mapper.Map<Tv>(source: entity);
        }

        public virtual async Task<int> Add(Tv view)
        {
            var entity = _mapper.Map<Te>(source: view);
            await _unitOfWork.GetRepositoryAsync<Te>().Insert(entity);
            await _unitOfWork.SaveAsync();
            return entity.Id;
        }

        public async Task<int> Update(Tv view)
        {
            await _unitOfWork.GetRepositoryAsync<Te>().Update(view.Id, _mapper.Map<Te>(source: view));
            return await _unitOfWork.SaveAsync();
        }

        public virtual async Task<int> Remove(int id)
        {
            Te entity = await _unitOfWork.Context.Set<Te>().FindAsync(id);
            await _unitOfWork.GetRepositoryAsync<Te>().Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public virtual async Task<IEnumerable<Tv>> Get(Expression<Func<Te, bool>> predicate)
        {
            var items = await _unitOfWork.GetRepositoryAsync<Te>()
                .Get(predicate: predicate);
            return _mapper.Map<IEnumerable<Tv>>(source: items);
        }

    }
}
