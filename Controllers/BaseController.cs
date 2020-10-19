using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestDimaBack.Models;

public abstract class BaseController : Controller
{
  public ApplicationContext db;

  public BaseController(ApplicationContext context)
  {
    db = context;
  }
  
  public override void OnActionExecuted(ActionExecutedContext context)
  {
    if (context.HttpContext.Request.Headers.ContainsKey("User-Agent"))
    {
      var userAgent = context.HttpContext.Request.Headers["User-Agent"].FirstOrDefault();

      if (userAgent.Contains("MSIE") || userAgent.Contains("Trident"))
      {
        context.Result = Content("Internet Explorer не поддерживается");
      }
    }
    base.OnActionExecuted(context);
  }
}