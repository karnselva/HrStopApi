using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HrApi.QueryParameters
{
    public class SearchEmployeeQueryParameters
    {
        [BindRequired]
        public string Name { get; set; } = null!;
    }
}
