using GymManagement.Data;
using GymManagement.Domain;

namespace GymManagement.IRepositories.Repositories
{
    public class SubscriptionTypeRepository:GeneralRepository<SubscriptionType>, ISubscriptionTypeRepository
    {
        public SubscriptionTypeRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            
        }
    }
}
