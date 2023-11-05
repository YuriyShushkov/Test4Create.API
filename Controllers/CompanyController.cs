using Microsoft.AspNetCore.Mvc;
using Test4Create.API.Models.Company;
using Test4Create.API.Services.Processing;

namespace Test4Create.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyProcessingService _companyProcessingService;

        public CompanyController(ICompanyProcessingService companyProcessingService) => _companyProcessingService = companyProcessingService;

        [HttpPost]
        public async Task<int> Create(CreateCompanyProcessingRequest request) => await _companyProcessingService.CreateAsync(request);
    }
}
