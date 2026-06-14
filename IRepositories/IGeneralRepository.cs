using GymManagement.Domain;
using GymManagement.Dtos;
using System.Linq.Expressions;

namespace GymManagement.IRepositories
{
    public interface IGeneralRepository<T> where T : BaseEntity
    {
        public Task<T> CreateNewAsync(T entity);
        Task<T?> GetTById(int id, params Expression<Func<T, object>>[] includes);
        public Task UpdateAsync(T entity);
        Task Delete(T entity);
        Task<List<T>> SchedulingByDayId(int dayId, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<List<T>?> GetAllByCondition(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] Includes);
    }
}
