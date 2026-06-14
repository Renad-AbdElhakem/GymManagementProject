using GymManagement.Domain;

namespace GymManagement.IRepositories
{
    public interface ICourseRepository:IGeneralRepository<Course>
    {
        Task<Course?> SearchCourseByName(string courseName);
    }
}
