using HrApi.DTOs;
using HrApi.Models;

namespace HrApi.Profiles
{
    public class EmployeesCredentialProfile:Profile
    {
        public EmployeesCredentialProfile() {

            CreateMap<EmployeesCredential,CreateCredentialDTO>().ReverseMap();
        } 
    }
}
