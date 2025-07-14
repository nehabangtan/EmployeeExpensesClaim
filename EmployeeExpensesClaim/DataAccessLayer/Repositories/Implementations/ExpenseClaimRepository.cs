using EmployeeExpensesClaim.DataAccessLayer.DbContexts;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeExpensesClaim.DataAccessLayer.Repositories.Implementations
{
   
        public class ExpenseClaimRepository : IExpenseClaimRepository
        {
            private readonly ExpenseDbContext _context;

            public ExpenseClaimRepository(ExpenseDbContext context)
            {
                _context = context;
            }

            public async Task<ExpenseClaim?> GetByIdAsync(int id)
            {
                return await _context.ExpenseClaims
                    .Include(ec => ec.Emp)
                    .Include(ec => ec.FilesTables)
                    .Include(ec => ec.AuditLogs)
                    .FirstOrDefaultAsync(ec => ec.Id == id);
            }

            public async Task<List<ExpenseClaim>> GetByEmployeeIdAsync(int empId)
            {
                return await _context.ExpenseClaims
                    .Where(ec => ec.EmpId == empId)
                    .ToListAsync();
            }

            public async Task CreateAsync(ExpenseClaim claim)
            {
                await _context.ExpenseClaims.AddAsync(claim);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(ExpenseClaim claim)
            {
                _context.ExpenseClaims.Update(claim);
                await _context.SaveChangesAsync();
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var claim = await _context.ExpenseClaims.FindAsync(id);
                if (claim == null)
                    return false;

                _context.ExpenseClaims.Remove(claim);
                await _context.SaveChangesAsync();
                return true;
            }
    }
}
