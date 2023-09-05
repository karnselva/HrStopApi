using HrApi.DTOs;
using HrApi.Models;

namespace HrApi.Profiles
{
    public class FamiliesDetailProfiles:Profile
    {
        public FamiliesDetailProfiles()
        {
            CreateMap<EmployeesDetailsAdmin, CreateEmployeeDetailsAdminDTO>().ReverseMap();
        }
    }
}
