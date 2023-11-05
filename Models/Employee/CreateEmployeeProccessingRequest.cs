using Test4Create.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Test4Create.API.Models.Employee
{
    public class CreateCompanyListRequest
    {
        public string? Name { get; set; }
        public int? Id { get; set; }
    }

    public class CreateEmployeeProccessingRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public EmployeeTitleEnum Title { get; set; }

        public IEnumerable<CreateCompanyListRequest> Companies { get; set; }
    }
}
