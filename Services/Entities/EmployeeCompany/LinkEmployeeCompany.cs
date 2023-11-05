using Test4Create.API.Data;
using Test4Create.API.Entities;

namespace Test4Create.API.Services.Entities.EmployeeCompany
{
    public interface ILinkEmployeeCompany
    {
        Task CreateLink(int companyId, int employeeId);
    }

    public class LinkEmployeeCompany : ILinkEmployeeCompany
    {
        private CompanyContext _dbContext;
        private readonly IValidateLinkEmployeeCompany _validationService;

        public LinkEmployeeCompany(CompanyContext dbContext, IValidateLinkEmployeeCompany validationService)
        {
            _dbContext = dbContext;
            _validationService = validationService;
        }

        public async Task CreateLink(int companyId, int employeeId)
        {
            await _validationService.ValidateAsync(companyId, employeeId).ConfigureAwait(false);
            _dbContext.CompanyEmployee.Add(new CompanyEmployee { CompanyId = companyId, EmployeeId = employeeId });
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}
