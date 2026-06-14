using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.AutoMapper
{
    public class SubscriptionTypeMappingProfile :Profile
    {
        public SubscriptionTypeMappingProfile()
        {
            
            CreateMap<SubscriptionTypeRequestDto, SubscriptionType>();
            CreateMap<SubscriptionType, SubscriptionTypeResponseDto>();
        
        }
    }
}
