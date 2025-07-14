using AutoMapper;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.ViewModels;

namespace EmployeeExpensesClaim
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {         
            CreateMap<Employee, EmployeeViewModel>();

            CreateMap<EmployeeViewModel, Employee>();

            CreateMap<ExpenseClaim, ExpenseClaimViewModel>().ReverseMap();
        }
    }
}
