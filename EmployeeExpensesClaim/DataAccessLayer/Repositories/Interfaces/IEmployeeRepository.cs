using EmployeeExpensesClaim.DataAccessLayer.Entities;

namespace EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        
      Task<Employee?> GetEmployeeByIdRepoAsync(int id);
      Task UpdateAsync(Employee emp);
        
    }
}
