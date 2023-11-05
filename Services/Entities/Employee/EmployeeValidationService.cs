using Microsoft.EntityFrameworkCore;
using Test4Create.API.Data;
using Test4Create.API.Helpers;
using Test4Create.API.Models.Employee;

namespace Test4Create.API.Services
{
    public interface IEmployeeValidationService
    {
        Task ValidateEmployeeAsync(CreateEmployeeRequest model);
    }

    public class EmployeeValidationService : IEmployeeValidationService
    {
        private CompanyContext _dbContext;

        public EmployeeValidationService(CompanyContext dbContext) => _dbContext = dbContext;

        public async Task ValidateEmployeeAsync(CreateEmployeeRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new RepositoryException($"Email is required");
            }

            if (await _dbContext.Employees.AnyAsync(x => x.Email == model.Email))
                throw new RepositoryException($"A Employee with the Email {model.Email} already exist in the database");
        }
    }
}
