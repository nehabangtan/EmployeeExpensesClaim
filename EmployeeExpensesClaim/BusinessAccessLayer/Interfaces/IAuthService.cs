using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.ViewModels;

namespace EmployeeExpensesClaim.BusinessAccessLayer.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseViewModel<EmployeeViewModel>> AssignAdminRoleAsync(int empId);
        Task<string> Authenticate(EmployeeViewModel user);
    }
}
