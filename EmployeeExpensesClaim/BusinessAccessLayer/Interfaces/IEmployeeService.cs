using EmployeeExpensesClaim.DataAccessLayer.Entities;

namespace EmployeeExpensesClaim.BusinessAccessLayer.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee?> GetEmployeeByIdAsync(int id);
    }
}
