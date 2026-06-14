using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.IServices
{
    public interface IRoleService
    {

        Task<GeneralResponse<RoleDto>> CreateRole(CreateRoleDto createRoleDto);
        Task<GeneralResponse<RoleDto>> GetRoleById(int id);
        Task<GeneralResponse<List<RoleDto>>> GetAllRoles();
    }
}
