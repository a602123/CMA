using CMA.Common.Model;
using CMA.WebSite.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMA.WebSite.Web.Controllers
{
    public class UserController : Controller
    {
        private UserBusiness _business = new UserBusiness();
        private RoleBusiness _rolebusiness = new RoleBusiness();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

       

        public ActionResult UserInfo()
        {
            return View();
        }

        public ActionResult Edit(long id)
        {
            //return View();
            try
            {
                var model = _business.Get(id);
                ViewBag.RoleList = new RoleBusiness().GetList();
                return View(model);
            }
            catch (AppException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "未知错误，请联系管理人员";
                return View();
            }
        }
         
        [HttpPost]
        public ActionResult Edit(UserInfoModel model)
        {
            try
            {
                _business.Edit(model);
                return Json(new { State = true });
            }
            catch (AppException ex)
            {
                return Json(new { State = true, Message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new { State = true, Message = "未知错误，请联系管理人员" });
            }
        }       

        public ActionResult Add()
        {
            List<SelectListItem> roleSelectList = new List<SelectListItem>();
            (_rolebusiness.GetList() as List<RoleModel>).
             ForEach(r=>roleSelectList.Add(new SelectListItem { Text = r.Name, Value = r.Id.ToString()}
              ));
            ViewBag.Rolelist = roleSelectList;
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserInfoModel model)
        {
            try
            {
                    _business.Add(model);
                    return Json(new { State = true, Message = "已成功添加用户！" });
            }
            catch (AppException ex)
            {
                return Json(new { State = false, Message = $"添加用户失败：{ex.Message}" });
            }
            catch (Exception)
            {
                return Json(new { State = false, Message = "未知错误，请联系管理人员" });
            }
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                _business.Delete(id);
                return Json(new { State = true, Message="删除成功!" });
            }
            catch (AppException ex)
            {
                return Json(new { State = false, Message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new { State = false, Message = "未知错误，请联系管理人员" });
            }
        }
        
        [HttpPost]
        public ActionResult RestPassword(long id)
        {
            try
            {
                _business.RestPassword(id);
                return Json(new { State = true, Message = "密码重置成功!"});
            }
            catch (AppException ex)
            {
                return Json(new { State = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { State = false, Message = $"未知错误，请联系管理人员msg:{ex.Message}" });
            }
        }

        public JsonResult GetList()
        {
            var list = _business.GetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}