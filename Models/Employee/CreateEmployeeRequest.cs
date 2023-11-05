using Test4Create.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Test4Create.API.Models.Employee
{
       public class CreateEmployeeRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public EmployeeTitleEnum Title { get; set; }
    }
}
