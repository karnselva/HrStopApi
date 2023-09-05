using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HrApi.Attribute
{
    // actionfilterattribute is used to filter the data and
    // only which satisfied condition will be in result
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        //onactionexecuting is call before iaction start
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //context refer to model or dto from user 
            //modelstate refer to object of all data of error which it violate data annotation
            // model state has isvalid method if false return  modelstate data

            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
            }

        }
    }
}
