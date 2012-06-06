using System.Web.Mvc;

namespace KangaModeling.Web.Controllers
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            
            base.OnActionExecuting(filterContext);
        }
    }
}