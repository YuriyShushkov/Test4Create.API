using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Test4Create.API.Data;
using Test4Create.API.Helpers;
using Test4Create.API.Models.Employee;

namespace Test4Create.API.Services.Entities.Employee
{
    public interface IEmployeeService
    {
        Task<int> CreateAsync(CreateEmployeeRequest model);
    }

    public class EmployeeService : IEmployeeService
    {
        private CompanyContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISystemLogService _systemLogService;

        public EmployeeService(CompanyContext dbContext, IMapper mapper, ISystemLogService systemLogService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _systemLogService = systemLogService;

        }

        public async Task<int> CreateAsync(CreateEmployeeRequest request)
        {
            if (await _dbContext.Employees.AnyAsync(x => x.Email == request.Email))
                throw new RepositoryException($"An employee with the Email {request.Email} already exists.");

            var employee = _mapper.Map<API.Entities.Employee>(request);

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);

            await _systemLogService.CreateAsync(employee);

            return employee.Id;
        }
    }
}
