using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;

namespace GymManagement.IServices.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public async Task<GeneralResponse<CourseDto>> CreateCourse(CreateCourseDto createCourseDto)
        {
            var newCourse = _mapper.Map<Course>(createCourseDto);
            await _courseRepository.CreateNewAsync(newCourse);
            var courseDto=_mapper.Map<CourseDto>(newCourse);
            return GeneralResponse<CourseDto>.Succsess(courseDto);
        }

        public async Task<GeneralResponse<AssignCoachCourseDto>> UpdateCourseWithAssignCoah(AssignCoachCourseDto assignCoach)
        {
            var findCourse = await _courseRepository.SearchCourseByName(assignCoach.CourseName);
            if (findCourse == null)
                return GeneralResponse<AssignCoachCourseDto>.ErrorResponse("Course Not Found");


            var findEmployee = await _employeeRepository.GetTById(assignCoach.EmployeeId);
            if (findEmployee == null)
                return GeneralResponse<AssignCoachCourseDto>.ErrorResponse("Employee Not Found");


            findCourse.EmployeeId = assignCoach.EmployeeId;

            await _courseRepository.UpdateAsync(findCourse);

            return GeneralResponse<AssignCoachCourseDto>.Succsess(assignCoach, "Coach assigned");
        }

        public async Task<GeneralResponse<List<CourseDto>>> GetAllCourse()
        {
            var coursesList = await _courseRepository.GetAll(c => c.Employee);

            if (!coursesList.Any())
                return GeneralResponse<List<CourseDto>>.ErrorResponse("No class found");

            var courseListDto = _mapper.Map<List<CourseDto>>(coursesList);

            return GeneralResponse<List<CourseDto>>.Succsess(courseListDto);

        }
      
        public async Task<GeneralResponse<CourseDto>> GetCourseById(int id)
        {

            var course = await _courseRepository.GetTById(id,c=>c.Employee);
            var co = _mapper.Map<CourseDto>(course);

            if (co == null)
                return GeneralResponse<CourseDto>.ErrorResponse("Course Not Found");

            return GeneralResponse<CourseDto>.Succsess(co);

        }





    }
}
