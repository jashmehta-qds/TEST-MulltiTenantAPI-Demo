using AutoMapper;
using TEST_MulltiTenantAPI_Demo.Entity;
using System;

namespace TEST_MulltiTenantAPI_Demo.Domain.Mapping
{
    public partial class MappingProfile : Profile
    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            #region MasterDb
            CreateMap<AccountViewModel, Account>();
            CreateMap<Account, AccountViewModel>();
            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.DecryptedPassword, opts => opts.MapFrom(src => src.Password))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => string.Join(";", src.Roles)));
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Password, opts => opts.MapFrom(src => src.DecryptedPassword))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Roles.Split(";", StringSplitOptions.RemoveEmptyEntries)));
            #endregion

            #region SlaveDb
            CreateMap<TaxAccountNumberViewModel, TaxAccountNumber>();
            CreateMap<TaxAccountNumber, TaxAccountNumberViewModel>();
            #endregion

            //call code in partial scaffolded function
            SetAddedMappingProfile();
        }

        //to call scaffolded method
        partial void SetAddedMappingProfile();

    }





}
