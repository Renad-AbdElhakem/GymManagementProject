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

        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes);
    }
}
