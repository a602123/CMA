using CMA.Common.Model;
using CMA.WebSite.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMA.WebSite.Web.Controllers
{
    public class CollectorController : Controller
    {

        private CollectorBusiness _business = new CollectorBusiness();

        // GET: Collect
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            try
            {
                IEnumerable<CollectorDBModel> list = _business.GetList();
                return Json(list.ToList(), JsonRequestBehavior.AllowGet);
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
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(CollectorDBModel model)
        {
            try
            {
                _business.Add(model);
                return Json(new { State = true, Message = "已经成功添加了一个采集器" });
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
    }
}