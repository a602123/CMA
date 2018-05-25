using CMA.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMA.WebSite.Web
{
    
    public class CheckLoginFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断控制器是否贴有SkipLoginAttr特性标签 如果有跳转验证
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipLoginAttr),false))
            {
                return;
            }
            //判断行为是否贴有SkipLoginAttr特性标签 如果有跳转验证
            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipLoginAttr), false))
            {
                return;
            }
            if (filterContext.HttpContext.Request.Cookies[Keys.UserCookieKey] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}