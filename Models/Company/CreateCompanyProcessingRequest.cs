using Test4Create.API.Entities;

namespace Test4Create.API.Models.Company
{
    public class CreateEmployeeListRequest
    {
        public string? Email { get; set; }
        public EmployeeTitleEnum? Title { get; set; }
        public int? Id { get; set; }
    }

    public class CreateCompanyProcessingRequest
    {
        public string Name { get; set; }
        public IEnumerable<CreateEmployeeListRequest> Employees { get; set; }
    }
}