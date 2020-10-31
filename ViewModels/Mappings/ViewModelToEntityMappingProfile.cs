
using KinderKulturServer.Models.Entities;
using AutoMapper;
 

namespace KinderKulturServer.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<AppUser, RegistrationViewModel>().ForMember(au => au.Email, map => map.MapFrom(vm => vm.UserName));
            CreateMap<Link, LinkViewModel>();
        }
    }
}
