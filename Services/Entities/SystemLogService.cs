using Test4Create.API.Data;
using Test4Create.API.Entities;

namespace Test4Create.API.Services.Entities
{
    public interface ISystemLogService
    {
        Task CreateAsync(API.Entities.Company company);
        Task CreateAsync(API.Entities.Employee employee);
    }

    public class SystemLogService: ISystemLogService
    {
        private CompanyContext _dbContext;

        public SystemLogService(CompanyContext dbContext) => _dbContext = dbContext;

        public async Task CreateAsync(API.Entities.Company company)
        {
            var log = new SystemLog
            {
                ResourceType = ResourceTypeEnum.Company,
                CreatedAt = company.CreatedAt,
                ResourceAttributes = string.Join(",", GetAttributesBase(company).Concat(GetAttributes(company))),
                Event = EventEnum.Create,
                Comment = $"New company '{company.Name}' was created"
            };

            _dbContext.SystemLog.Add(log);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }

        public async Task CreateAsync(API.Entities.Employee employee)
        {
            var log = new SystemLog
            {
                ResourceType = ResourceTypeEnum.Employee,
                CreatedAt = employee.CreatedAt,
                ResourceAttributes = string.Join(",", GetAttributesBase(employee).Concat(GetAttributes(employee))),
                Event = EventEnum.Create,
                Comment = $"New employee '{employee.Email}' was created"
            };

            _dbContext.SystemLog.Add(log);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        private IEnumerable<string> GetAttributesBase(Base entity)
        {
            yield return $"Id = '{entity.Id}'";
            yield return $"CreatedAt = '{entity.CreatedAt}'";
        }

        private IEnumerable<string> GetAttributes(API.Entities.Company company)
        {
            yield return $"Name = '{company.Name}'";
        }

        private IEnumerable<string> GetAttributes(API.Entities.Employee employee)
        {
            yield return $"Email = '{employee.Email}'";
            yield return $"Title = '{employee.Title}'";
        }
    }
}
