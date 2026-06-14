using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;

namespace GymManagement.IServices.Services
{
    public class WeekDaysService : IWeekDaysService
    {
        private readonly IWeekDaysRepository _daysRepository;
        private readonly IMapper _mapper;

        public WeekDaysService(IWeekDaysRepository daysRepository, IMapper mapper)
        {
            _daysRepository = daysRepository;
            _mapper = mapper;
        }



        public async Task<GeneralResponse<DaysDto>> CreateNewDay(CreateWeekDaysDto createDayDto)
        {

            var addDay = _mapper.Map<WeekDays>(createDayDto);
            await _daysRepository.CreateNewAsync(addDay);
            var dayDto = _mapper.Map<DaysDto>(addDay);
            return GeneralResponse<DaysDto>.Succsess(dayDto, "Day Added Done");

        }
        public async Task<GeneralResponse<DaysDto>> FindDayByName(string dayName)
        {
            var day = await _daysRepository.SearchByName(dayName);
            var dayDto = _mapper.Map<DaysDto>(day);
            if (dayDto == null) 
                return GeneralResponse<DaysDto>.ErrorResponse("Day not found");
          
            return GeneralResponse<DaysDto>.Succsess(dayDto);

        }



    }
}
