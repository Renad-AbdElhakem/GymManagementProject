using GymManagement.Data;
using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.IRepositories.Repositories
{
    public class CourseRepository : GeneralRepository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Course?> SearchCourseByName(string courseName)
        {
            var course = await _dbSet.FirstOrDefaultAsync(c => c.CourseName.Contains(courseName));
            if (course == null)  
                return null;
            
            return course;
        }
    }
}
