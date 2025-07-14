using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.DataAccessLayer.DbContexts;
using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
using System;

namespace EmployeeExpensesClaim.DataAccessLayer.Repositories.Implementations
{

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ExpenseDbContext _context;
        public EmployeeRepository(ExpenseDbContext context) => _context = context;

        public async Task<Employee?> GetEmployeeByIdRepoAsync(int id) =>
            await _context.Employees.FindAsync(id);

        public async Task UpdateRepoAsync(Employee emp)
        {
            _context.Employees.Update(emp);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRepoAsync(Employee emp)
        {
            await _context.Employees.AddAsync(emp);
            await _context.SaveChangesAsync();
        }

        public void DeleteRepoAsync(Employee emp)
        {
            if (emp == null) throw new ArgumentNullException(nameof(emp));
            _context.Employees.Remove(emp);
            _context.SaveChanges();

        }
    }
}
