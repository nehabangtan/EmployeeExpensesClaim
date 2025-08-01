using EmployeeExpensesClaim.DataAccessLayer.DbContexts;
using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeExpensesClaim.DataAccessLayer.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ExpenseDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ExpenseDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllByPropertyAsync(string propertyName, int value)
        {
            return await _dbSet
                .Where(e => EF.Property<int>(e, propertyName) == value)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();


        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);

        public void Update(T entity) => _dbSet.Update(entity);

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            return true;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}

