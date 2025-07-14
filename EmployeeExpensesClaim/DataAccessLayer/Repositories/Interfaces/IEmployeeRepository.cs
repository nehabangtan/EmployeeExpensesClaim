using EmployeeExpensesClaim.DataAccessLayer.Entities;

namespace EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        
      Task<Employee?> GetEmployeeByIdRepoAsync(int id);
      Task UpdateRepoAsync(Employee emp);

      Task CreateRepoAsync(Employee emp);

      void DeleteRepoAsync(Employee emp);   

    }
}
