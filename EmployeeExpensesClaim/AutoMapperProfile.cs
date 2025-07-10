using AutoMapper;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.ViewModels;

namespace EmployeeExpensesClaim
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Entity to ViewModel
            CreateMap<Employee, EmployeeViewModel>()
              .ForMember(dest => dest.EmpRole, opt => opt.MapFrom(src => src.EmpRole));

        }
    }
}
