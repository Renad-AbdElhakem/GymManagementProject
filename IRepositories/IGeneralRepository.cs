using GymManagement.Dtos;
using System.Linq.Expressions;

namespace GymManagement.IRepositories
{
    public interface IGeneralRepository<T>
    {
        public Task<T> CreateNewAsync(T entity);
        Task<T?> GetTById(int id, params Expression<Func<T, object>>[] includes);
        public Task UpdateAsync(T entity);

        public Task<List<T>> GetAll();
    }
}
