using EmployeeExpensesClaim.DataAccessLayer.Entities;

namespace EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces
{
    public interface IExpenseClaimRepository
    {
        Task<ExpenseClaim?> GetByIdAsync(int id);
        Task<List<ExpenseClaim>> GetByEmployeeIdAsync(int empId);
        Task CreateAsync(ExpenseClaim claim);
        Task UpdateAsync(ExpenseClaim claim);
        Task<bool> DeleteAsync(int id);
    }
}
