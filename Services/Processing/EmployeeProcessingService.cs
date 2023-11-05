using AutoMapper;
using Test4Create.API.Data;
using Test4Create.API.Models.Company;
using Test4Create.API.Models.Employee;
using Test4Create.API.Services.Entities.Company;
using Test4Create.API.Services.Entities.Employee;
using Test4Create.API.Services.Entities.EmployeeCompany;

namespace Test4Create.API.Services.Processing
{
    public interface IEmployeeProcessingService
    {
        Task<int> CreateAsync(CreateEmployeeProccessingRequest request);
    }

    public class EmployeeProcessingService : IEmployeeProcessingService
    {
        private CompanyContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly IEmployeeService _employeeService;
        private readonly ILinkEmployeeCompany _linkEmployeeCompany;

        public EmployeeProcessingService(
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

        public async Task<int> CreateAsync(CreateEmployeeProccessingRequest request)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                var employeeId = await _employeeService.CreateAsync(_mapper.Map<CreateEmployeeRequest>(request)).ConfigureAwait(false);

                foreach (var company in request.Companies)
                {
                    var companyId = company.Id.HasValue ? company.Id : await _companyService.CreateAsync(_mapper.Map<CreateCompanyRequest>(company)).ConfigureAwait(false);
                    await _linkEmployeeCompany.CreateLink(companyId.Value, employeeId).ConfigureAwait(false);
                }

                await trans.CommitAsync().ConfigureAwait(false);

                return employeeId;
            }
            catch
            {
                await trans.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }
    }
}
