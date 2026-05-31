using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;

namespace GymManagement.IServices.Services
{
    public class SubscriptionTypeService: ISubscriptionTypeService
    {
        private readonly ISubscriptionTypeRepository _repo;
        private readonly IMapper _mapper;

        public SubscriptionTypeService(ISubscriptionTypeRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<SubscriptionTypeResponseDto>> CreateAsync(SubscriptionTypeRequestDto dto)
        {
            var entity = _mapper.Map<SubscriptionType>(dto);
            var created = await _repo.CreateNewAsync(entity);
            var response = _mapper.Map<SubscriptionTypeResponseDto>(created);
            return GeneralResponse<SubscriptionTypeResponseDto>.Succsess(response, "Subscription plan created successfully.");
        }

        public async Task<GeneralResponse<SubscriptionTypeResponseDto>> UpdateAsync(int id, SubscriptionTypeRequestDto dto)
        {
            var entity = await _repo.GetTById(id);
            if (entity == null)
                return GeneralResponse<SubscriptionTypeResponseDto>.ErrorResponse($"No subscription plan found with id {id}.");

            _mapper.Map(dto, entity); 
            await _repo.UpdateAsync(entity);

            var response = _mapper.Map<SubscriptionTypeResponseDto>(entity);
            return GeneralResponse<SubscriptionTypeResponseDto>.Succsess(response, "Subscription plan updated successfully.");
        }

        public async Task<GeneralResponse<SubscriptionTypeResponseDto>> GetByIdAsync(int id)
        {
            var entity = await _repo.GetTById(id);
            if (entity == null)
                return GeneralResponse<SubscriptionTypeResponseDto>.ErrorResponse($"No subscription plan found with id {id}.");

            var response = _mapper.Map<SubscriptionTypeResponseDto>(entity);
            return GeneralResponse<SubscriptionTypeResponseDto>.Succsess(response);
        }

        public async Task<GeneralResponse<List<SubscriptionTypeResponseDto>>> GetAllAsync()
        {
            var entities = await _repo.GetAll();
            var response = _mapper.Map<List<SubscriptionTypeResponseDto>>(entities);
            return GeneralResponse<List<SubscriptionTypeResponseDto>>.Succsess(response);
        }
    }
}

