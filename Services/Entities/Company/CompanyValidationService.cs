using Microsoft.EntityFrameworkCore;
using Test4Create.API.Data;
using Test4Create.API.Helpers;
using Test4Create.API.Models.Company;

namespace Test4Create.API.Services.Entities.Company
{
    public interface ICompanyValidationService
    {
        Task ValidateCompanyAsync(CreateCompanyRequest model);
    }

    public class CompanyValidationService : ICompanyValidationService
    {
        private CompanyContext _dbContext;

        public CompanyValidationService(CompanyContext dbContext) => _dbContext = dbContext;

        public async Task ValidateCompanyAsync(CreateCompanyRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new RepositoryException($"Name is required");
            }

            if (await _dbContext.Company.AnyAsync(x => x.Name == model.Name))
                throw new RepositoryException($"A company with the Name {model.Name} already exist in the database");
        }
    }
}
