using AutoMapper;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterVM, Account>();
        }

       
    }
}
