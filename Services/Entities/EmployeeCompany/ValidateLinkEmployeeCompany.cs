using Microsoft.EntityFrameworkCore;
using Test4Create.API.Data;
using Test4Create.API.Helpers;

namespace Test4Create.API.Services.Entities.EmployeeCompany
{
    public interface IValidateLinkEmployeeCompany
    {
        Task ValidateAsync(int employeeId, int companyId);
    }

    public class ValidateLinkEmployeeCompany : IValidateLinkEmployeeCompany
    {
        private CompanyContext _dbContext;

        public ValidateLinkEmployeeCompany(CompanyContext dbContext) => _dbContext = dbContext;

        public async Task ValidateAsync(int companyId, int employeeId)
        {
            if (await _dbContext.CompanyEmployee.AnyAsync(x => x.EmployeeId == employeeId && x.CompanyId == companyId).ConfigureAwait(false))
                throw new RepositoryException($"Employee(Id = {employeeId}) already add to company(Id = {companyId})");

            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId).ConfigureAwait(false);
            if (employee is null)
                throw new RepositoryException($"Don't find Employee by Id {employeeId})");

            var company = await _dbContext.Company.FirstOrDefaultAsync(x => x.Id == companyId).ConfigureAwait(false);
            if (company is null)
                throw new RepositoryException($"Don't find Company by Id {companyId})");

            var hasSameTitle = await _dbContext.CompanyEmployee
                .Where(ce => ce.CompanyId == companyId)
                .Join(_dbContext.Employees, ce => ce.EmployeeId, e => e.Id, (ce, e) => e)
                .AnyAsync(x => x.Title == employee.Title)
                .ConfigureAwait(false);

            if (hasSameTitle)
                throw new RepositoryException($"Company (Id = {companyId}) already has employee with title ({employee.Title})");
        }
    }
}
