using CMA.Common.Model;
using CMA.WebSite.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMA.WebSite.Web.Controllers
{
    public class MessageController : Controller
    {
        private MessageBusiness _business = new MessageBusiness();
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList()
        {
            var list = _business.GetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Add(MessageModel model)
        {
            try
            {
                _business.Add(model);
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
        public ActionResult Edit(int  id)
        {
            //return View();
            try
            {
                var model = _business.Get(id);
                ViewBag.UserModel = model;
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
        public ActionResult Edit(MessageModel model)
        {
            try
            {
                _business.Edit(model);
                return Json(new { State = true, Message="修改成功"});
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