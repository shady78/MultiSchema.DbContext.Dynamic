using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Interfaces;

namespace WebApplication3.ActionFilteres;

// 4. إنشاء Action Filter للتحقق من البارامترات العامة
public class GlobalParameterActionFilter : IActionFilter
{
    private readonly IGlobalParameterService _parameterService;

    public GlobalParameterActionFilter(IGlobalParameterService parameterService)
    {
        _parameterService = parameterService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var schema = _parameterService.GetSchemaName();
        if (string.IsNullOrEmpty(schema))
        {
            context.Result = new BadRequestObjectResult("Schema parameter is required");
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // يمكن إضافة منطق ما بعد تنفيذ الـ Action
        _parameterService.SetSchemaName(null);
    }
}