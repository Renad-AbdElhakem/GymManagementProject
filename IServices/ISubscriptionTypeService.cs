using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.IServices
{
    public interface ISubscriptionTypeService
    {
        Task<GeneralResponse<SubscriptionTypeResponseDto>> CreateAsync(SubscriptionTypeRequestDto dto);
        Task<GeneralResponse<SubscriptionTypeResponseDto>> UpdateAsync(int id, SubscriptionTypeRequestDto dto);
        Task<GeneralResponse<SubscriptionTypeResponseDto>> GetByIdAsync(int id);
        Task<GeneralResponse<List<SubscriptionTypeResponseDto>>> GetAllAsync();
    }
}
