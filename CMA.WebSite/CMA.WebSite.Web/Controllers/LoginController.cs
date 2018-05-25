using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMA.WebSite.Business;
using CMA.Common.Model;

namespace CMA.WebSite.Web.Controllers
{   [SkipLoginAttr] 
    public class LoginController : Controller
    {
        private LoginBusiness _business = new LoginBusiness();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserInfoModel model)
        {
            try
            {
                _business.Login(model);
                HttpCookie Cookie = new HttpCookie(Keys.UserCookieKey, model.Id.ToString());
                Response.Cookies.Add(Cookie);
                return Json(new { State = true,msg="登录成功" });
            }
            catch (AppException ex)
            {
                return Json(new { State = false, msg = $"登陆失败：{ex.Message}" });
            }
            catch (Exception ex)
            {
                return Json(new { State = false, msg = "登陆失败：未知错误，请联系管理人员" });
            }
            

        }
    }
}