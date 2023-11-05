using AutoMapper;
using Test4Create.API.Data;
using Test4Create.API.Models.Company;

namespace Test4Create.API.Services.Entities.Company
{
    public interface ICompanyService
    {
        Task<int> CreateAsync(CreateCompanyRequest model);
    }

    public class CompanyService : ICompanyService
    {
        private CompanyContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICompanyValidationService _validationService;
        private readonly ISystemLogService _systemLogService;

        public CompanyService(CompanyContext dbContext, IMapper mapper, ICompanyValidationService validationService, ISystemLogService systemLogService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validationService = validationService;
            _systemLogService = systemLogService;

        }

        public async Task<int> CreateAsync(CreateCompanyRequest model)
        {
            await _validationService.ValidateCompanyAsync(model).ConfigureAwait(false);
            var company = _mapper.Map<API.Entities.Company>(model);

            _dbContext.Company.Add(company);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);

            await _systemLogService.CreateAsync(company);

            return company.Id;
        }
    }
}
