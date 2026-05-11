using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.IServices
{
    public interface ICourseService
    {
        Task<GeneralResponse<CourseDto>> CreateCourse(CreateCourseDto createCourseDto);
        Task<GeneralResponse<List<CourseDto>>> GetAllCourse();
        Task<GeneralResponse<AssignCoachCourseDto>> UpdateCourseWithAssignCoah(AssignCoachCourseDto assignCoach);
        Task<GeneralResponse<CourseDto>> GetCourseById(int id);


    }
}
