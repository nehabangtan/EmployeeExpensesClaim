using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.ViewModels;

namespace EmployeeExpensesClaim.BusinessAccessLayer.Interfaces
{
    public interface IEmployeeService
    {
        Task<ResponseViewModel<EmployeeViewModel>> GetEmployeeByIdAsync(int id);

        Task<ResponseViewModel<EmployeeViewModel>> CreateEmployeeAsync(EmployeeViewModel employeeViewModel);

        Task<ResponseViewModel<EmployeeViewModel>> DeleteEmployeeByIdAsync(int id);
    }
}
