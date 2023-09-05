using HrApi.DTOs;
using HrApi.Models;

namespace HrApi.Profiles
{
    public class EmployeesDetailsAdminProfile : Profile
    {
        public EmployeesDetailsAdminProfile() {

            CreateMap<EmployeesDetailsAdmin, CreateEmployeeDetailsAdminDTO>().ReverseMap();
        }
    }
}
