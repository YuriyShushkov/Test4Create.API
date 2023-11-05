using AutoMapper;
using Test4Create.API.Entities;
using Test4Create.API.Models.Company;
using Test4Create.API.Models.Employee;

namespace Test4Create.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateEmployeeProccessingRequest, CreateEmployeeRequest>();
            CreateMap<CreateEmployeeRequest, Employee>();
            CreateMap<CreateEmployeeListRequest, CreateEmployeeRequest>();

            CreateMap<CreateCompanyProcessingRequest, CreateCompanyRequest>();
            CreateMap<CreateCompanyRequest, Company>();
            CreateMap<CreateCompanyListRequest, CreateCompanyRequest>();
        }

    }
}