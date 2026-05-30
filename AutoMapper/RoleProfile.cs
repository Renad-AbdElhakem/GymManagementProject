using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
        }
    }
}
