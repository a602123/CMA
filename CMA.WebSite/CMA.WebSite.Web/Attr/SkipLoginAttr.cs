using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMA.WebSite.Web
{
    /// <summary>
    /// 自定义过滤器 用于跳转用户登录
    /// 只能贴在方法和控制器中
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple =false)]
    public class SkipLoginAttr:Attribute
    {
    }
}