using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.AutoMapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<AddNewEmployeeDto, Employee>();
      
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));

        }
    }
}
