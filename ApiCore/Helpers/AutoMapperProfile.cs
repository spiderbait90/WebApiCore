using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCore.Data.EntityModels;
using ApiCore.Data.ViewModels;
using AutoMapper;

namespace ApiCore.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserModel, User>();
            CreateMap<PutProductModel, Product>();
            CreateMap<PostProductModel, Product>();
            CreateMap<User, GetUserViewModel>();
        }
    }
}
