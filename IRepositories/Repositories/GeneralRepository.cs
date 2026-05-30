
using GymManagement.Data;
using GymManagement.Domain;
using GymManagement.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace GymManagement.IRepositories.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GeneralRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }


        public async Task<T> CreateNewAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetTById(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<List<T>> SchedulingByDayId(int dayId, params Expression<Func<T, object>>[] includes)

        {

            var query = _dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(s => s.Id == dayId).ToListAsync();

        }
        public async Task<List<T>?> GetAllByCondition(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            if (Includes != null && Includes.Any())
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
                
            }
            var result =  await query.Where(condition).ToListAsync();
            return result;

        }

    }
}
