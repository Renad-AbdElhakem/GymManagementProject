using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using System.Data;

namespace GymManagement.AutoMapper
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<AssignCoachCourseDto, Course>();
            CreateMap<CreateCourseDto, Course>();
            CreateMap<Course, CourseDto>()
                   .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Employee.UserName));
        }


    }
}
