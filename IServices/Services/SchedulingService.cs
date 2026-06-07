using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GymManagement.IServices.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly ISchedulingRepository _schedulingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICourseService _courseService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IWeekDaysRepository _dayRepository;

        public SchedulingService(ISchedulingRepository schedulingRepository,
                                 IWeekDaysRepository dayRepository,
                                 IEmployeeRepository employeeRepository,
                                 ICourseService courseService,
                                 IRoleService roleService,
                                 IMapper mapper)
        {
            _schedulingRepository = schedulingRepository;
            _dayRepository = dayRepository;
            _employeeRepository = employeeRepository;
            _courseService = courseService;
            _roleService = roleService;
            _mapper = mapper;
        }




        public async Task<GeneralResponse<SchedulingDto>> CreateTrainerDayScheduling(CreateSchedulingDto createSchedulingDto)
        {
            var day = await _dayRepository.SearchByName(createSchedulingDto.DayName);
            if (day == null)
                return GeneralResponse<SchedulingDto>.ErrorResponse("Day not found");

            var employee = await _employeeRepository.GetTById(createSchedulingDto.EmployeeId);
            if (employee == null)
                return GeneralResponse<SchedulingDto>.ErrorResponse("Employee not found");


            var addScheduling = new Scheduling
            {

                StartTime = createSchedulingDto.StartTime,
                EndTime = createSchedulingDto.EndTime,
                WeekDaysId = day.Id,
                EmployeeId = employee.Id,
            };

            if (!string.IsNullOrEmpty(createSchedulingDto.ClassName))
            {
                var findClass = await _courseService.GetClassByName(createSchedulingDto.ClassName);

                if (!findClass.Success)
                    return GeneralResponse<SchedulingDto>.ErrorResponse("class not found");

                addScheduling.CourseId = findClass.Data.Id;
            }


            await _schedulingRepository.CreateNewAsync(addScheduling);
            var schedullingDto = _mapper.Map<SchedulingDto>(addScheduling);
            return GeneralResponse<SchedulingDto>.Succsess(schedullingDto);

        }

        public Task<GeneralResponse<SchedulingDto>> DeleteSchedullingById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse<SchedulingDto>> SchedullingById(int id)
        {

            var schedulling = await _schedulingRepository.GetTById(id, x => x.Course,
                                                                       x => x.WeekDays);
            if (schedulling == null)
                return GeneralResponse<SchedulingDto>.ErrorResponse($"Not found Schedulling {id}");

            var schedullingDto = _mapper.Map<SchedulingDto>(schedulling);

            return GeneralResponse<SchedulingDto>.Succsess(schedullingDto);
        }

        public async Task<GeneralResponse<List<SchedulingDto>>> SchedullingList()
        {
            var schedulingList = await _schedulingRepository.GetAll(x => x.Course,
                                                                    x => x.WeekDays);

            if (schedulingList == null)
                return GeneralResponse<List<SchedulingDto>>.ErrorResponse("Empty scheduling ");

            var schedulingListDro = _mapper.Map<List<SchedulingDto>>(schedulingList);

            return GeneralResponse<List<SchedulingDto>>.Succsess(schedulingListDro);
        }

        public async Task<GeneralResponse<List<SchedulingDto>>> GetSchedulingByRoleId(int roleId)
        {
            var checkRoleId = await _roleService.GetRoleById(roleId);
            if (!checkRoleId.Success)
                return GeneralResponse<List<SchedulingDto>>.ErrorResponse("Not found role");

            List<Scheduling>? schedulingList;

            if (checkRoleId.Data.RoleName.Contains("Trainer"))
            {
                schedulingList = await _schedulingRepository.GetAllByCondition(
                    r => r.Employee.RoleId == roleId,
                    x => x.Course, x => x.WeekDays);
            }
            else
            {
                schedulingList = await _schedulingRepository.GetAllByCondition(
                    r => r.Employee.RoleId == roleId,
                    x => x.WeekDays);
            }

            if (schedulingList == null)
                return GeneralResponse<List<SchedulingDto>>.ErrorResponse("Empty scheduling");

            var schedulingListDto = _mapper.Map<List<SchedulingDto>>(schedulingList);
            return GeneralResponse<List<SchedulingDto>>.Succsess(schedulingListDto);
        }
        public async Task<GeneralResponse<SchedulingDto>> UpdateSchedullingById(int schedulingId, UpdateSchedulingDto updateSchedulingDto)
        {
            var scheduling = await _schedulingRepository.GetTById(schedulingId);
            if (scheduling == null)
                return GeneralResponse<SchedulingDto>.ErrorResponse("Scheduling not found");
            if (!string.IsNullOrEmpty(updateSchedulingDto.DayName))
            {
                var findDay = await _dayRepository.SearchByName(updateSchedulingDto.DayName);
                if (findDay == null)
                    return GeneralResponse<SchedulingDto>.ErrorResponse("Day not found");
                scheduling.WeekDaysId = findDay.Id;
            }

            if (!string.IsNullOrEmpty(updateSchedulingDto.ClassName))
            {

                var findClass = await _courseService.GetClassByName(updateSchedulingDto.ClassName);
                if (findClass == null)
                    return GeneralResponse<SchedulingDto>.ErrorResponse("Employee not found");
                scheduling.CourseId = findClass.Data.Id;
            }

            await _schedulingRepository.UpdateAsync(scheduling);

            var schedullingDto = _mapper.Map<SchedulingDto>(scheduling);

            return GeneralResponse<SchedulingDto>.Succsess(schedullingDto);
        }

        public async Task<GeneralResponse<List<SchedulingDto>>> SchedulingByDayName(string dayName, params Expression<Func<WeekDays, object>>[] includes)
        {

            var findDay = await _dayRepository.SearchByName(dayName);
            if (findDay == null)
                return GeneralResponse<List<SchedulingDto>>.ErrorResponse("Day not found");

            var schedulingList = await _schedulingRepository.SchedulingByDayId(findDay.Id, x => x.Course,
                                                                                           x => x.WeekDays);

            if (schedulingList == null)
                return GeneralResponse<List<SchedulingDto>>.ErrorResponse("Empty scheduling ");

            var schedulingListDro = _mapper.Map<List<SchedulingDto>>(schedulingList);

            return GeneralResponse<List<SchedulingDto>>.Succsess(schedulingListDro);
        }
        public async Task<GeneralResponse<List<SchedulingDto>>> SchedulingByClassId(int classId, params Expression<Func<WeekDays, object>>[] includes)
        {

            var findClass = await _courseService.GetCourseById(classId);
            if (findClass == null)
                return GeneralResponse<List<SchedulingDto>>.ErrorResponse("Day not found");

            var schedulingList = await _schedulingRepository.SchedulingByClassId(findClass.Data.Id, x => x.Course,
                                                                                           x => x.WeekDays);

            if (schedulingList == null)
                return GeneralResponse<List<SchedulingDto>>.ErrorResponse("Empty scheduling ");

            var schedulingListDto = _mapper.Map<List<SchedulingDto>>(schedulingList);

            return GeneralResponse<List<SchedulingDto>>.Succsess(schedulingListDto);
        }

        public async Task<GeneralResponse<List<SchedulingDto>>> SchedulingByEmployeeIdAndDayNameAsync(int employeeId, string dayName, params Expression<Func<Scheduling, object>>[] includes)
        {
            var employeeSchedulingList = await _schedulingRepository.SchedulingByEmployeeId(employeeId, dayName);
            if (employeeSchedulingList == null)
                return GeneralResponse<List<SchedulingDto>>
                    .ErrorResponse($"Empty scheduling for employee with Id : {employeeId} at day {dayName} ");

            var schedulingListDto = _mapper.Map<List<SchedulingDto>>(employeeSchedulingList);

            return GeneralResponse<List<SchedulingDto>>.Succsess(schedulingListDto);
        }
    }
}
