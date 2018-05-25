using CMA.Common.Model;
using CMA.WebSite.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMA.WebSite.Web.Controllers
{
    public class RoleController : Controller
    {
        private RoleBusiness _business = new RoleBusiness();
        private ActionBusiness _actionBusiness = new ActionBusiness();


        // GET: Role
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetList()
        {
            var list = _business.GetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long id)
        {
            try
            {
                var model = _business.Get(id);
                ViewBag.ActionList = _actionBusiness.GetList();
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
        public ActionResult Edit(RoleModel model)
        {
            try
            {
                _business.Edit(model);
                return Json(new { state = true });
            }
            catch (AppException ex)
            {
                return Json(new { State = true, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { State = true, Message = "未知错误，请联系管理人员" });
            }
        }
        [HttpGet]
        public ActionResult Add()
        {           
            try
            {             
                ViewBag.ActionList = _actionBusiness.GetList();
                return View();
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
        public ActionResult Add(RoleModel Rmodel)
        {
            try
            {
               
                _business.Add(Rmodel);
                return Json(new { State = true, Message = "添加成功" });
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

        public ActionResult Delete(int id)
        {
            try
            {
                _business.Delete(id);
                return Json(new { State = true, Message = "删除成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (AppException ex)
            {
                return Json(new { State = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { State = false, Message = "未知错误，请联系管理人员" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
    
}