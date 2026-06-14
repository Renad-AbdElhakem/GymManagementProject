using AutoMapper;
using GymManagement.Data;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.IServices.Services
{

    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoleService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<RoleDto>> CreateRole(CreateRoleDto createRoleDto)
        {
            var role = _mapper.Map<Role>(createRoleDto);
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            var dto = _mapper.Map<RoleDto>(role);
            return GeneralResponse<RoleDto>.Succsess(dto, "Role created successfully");
        }

        public async Task<GeneralResponse<RoleDto>> GetRoleById(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);
            if (role == null)
                return GeneralResponse<RoleDto>.ErrorResponse("Role not found");
            var dto = _mapper.Map<RoleDto>(role);
            return GeneralResponse<RoleDto>.Succsess(dto);
        }

        public async Task<GeneralResponse<List<RoleDto>>> GetAllRoles()
        {
            var roles = await _dbContext.Roles.ToListAsync();
            var dtos = _mapper.Map<List<RoleDto>>(roles);
            return GeneralResponse<List<RoleDto>>.Succsess(dtos);
        }
    }
}
