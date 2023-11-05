using Microsoft.AspNetCore.Mvc;
using Test4Create.API.Models.Employee;
using Test4Create.API.Services.Processing;

namespace Test4Create.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeProcessingService _employeeProcessingService;

        public EmployeeController(IEmployeeProcessingService employeeProcessingService)
        {
            _employeeProcessingService = employeeProcessingService;
        }

        // POST
        [HttpPost]
        public async Task<int> Create(CreateEmployeeProccessingRequest model) => await _employeeProcessingService.CreateAsync(model);
    }
}
