using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Controllers
{
    public class OriginHeaderFilter: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            actionExecutedContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, GET, POST, PUT, PATCH, DELETE");
            actionExecutedContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
        }
    }
}
