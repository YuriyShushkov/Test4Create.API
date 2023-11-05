using AutoMapper;
using Test4Create.API.Data;
using Test4Create.API.Models.Company;
using Test4Create.API.Models.Employee;
using Test4Create.API.Services.Entities.Company;
using Test4Create.API.Services.Entities.Employee;
using Test4Create.API.Services.Entities.EmployeeCompany;

namespace Test4Create.API.Services.Processing
{
    public interface ICompanyProcessingService
    {
        Task<int> CreateAsync(CreateCompanyProcessingRequest request);
    }

    public class CompanyProcessingService : ICompanyProcessingService
    {
        private CompanyContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly IEmployeeService _employeeService;
        private readonly ILinkEmployeeCompany _linkEmployeeCompany;

        public CompanyProcessingService(
            CompanyContext dbContext,
            IMapper mapper,
            ICompanyService companyService,
            IEmployeeService employeeService,
            ILinkEmployeeCompany linkEmployeeCompany)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _companyService = companyService;
            _employeeService = employeeService;
            _linkEmployeeCompany = linkEmployeeCompany;
        }

        public async Task<int> CreateAsync(CreateCompanyProcessingRequest request)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                var companyId = await _companyService.CreateAsync(_mapper.Map<CreateCompanyRequest>(request)).ConfigureAwait(false);

                foreach (var employee in request.Employees)
                {
                    var employeeId = employee.Id.HasValue ? employee.Id : await _employeeService.CreateAsync(_mapper.Map<CreateEmployeeRequest>(employee)).ConfigureAwait(false);
                    await _linkEmployeeCompany.CreateLink(companyId, employeeId.Value).ConfigureAwait(false);
                }

                await trans.CommitAsync().ConfigureAwait(false);
                return companyId;
            }
            catch
            {
                await trans.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }
    }
}
