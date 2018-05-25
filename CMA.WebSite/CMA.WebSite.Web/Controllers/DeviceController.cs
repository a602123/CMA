using CMA.Common.Model;
using CMA.WebSite.Business;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace CMA.WebSite.Web.Controllers
{
    public class DeviceController : Controller
    {
        private DeviceBusiness _business = new DeviceBusiness();

        private void GetIP()
        {
            List<string> list = new List<string>();
            string hostName = Dns.GetHostName();//本机名   
                                                //System.Net.IPAddress[] addressList = Dns.GetHostByName(hostName).AddressList;//会警告GetHostByName()已过期，我运行时且只返回了一个IPv4的地址   
            System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
            foreach (IPAddress ip in addressList)
            {
                list.Add(ip.ToString());
            }
        }

        // GET: Device
        public ActionResult Index()
        {
            GetIP();
            return View();
        }

        [HttpPost]
        public JsonResult Add(MonitorItemDBModel model)
        {
            try
            {
                MonitorItemType type = (MonitorItemType)Enum.Parse(typeof(MonitorItemType), Request.Form["typeSelect"].ToString().Trim());

                model = new MonitorItemDBModel();
                model.Name = Request.Form["Name"].ToString().Trim();
                model.Note = Request.Form["Note"].ToString().Trim();
                model.Host = Request.Form["MHost"].ToString().Trim();
                model.CollectorHost = Request.Form["CollectorHost"].ToString().Trim();
                model.ItemType = type;
                model.Paramter = GetParamterStr(Request, model.ItemType);

                bool b = _business.Add(model);
                return Json(new { State = b });
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
            try
            {
                dynamic viewModel = new ExpandoObject();

                Array arr = Enum.GetValues(typeof(MonitorItemType));
                List<object> temp = new List<object>();
                foreach (var item in arr)
                {
                    //temp.Add(new { name = EnumExtension.GetItemTypeName((MonitorItemType)Convert.ToInt32(item)), value = item });
                    temp.Add(item);
                }
                viewModel.typeList = temp;

                return View(viewModel);
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

        public JsonResult GetList()
        {
            try
            {
                IEnumerable<MonitorItemDBModel> list = _business.GetList();
                List<object> tempList = new List<object>();
                foreach (var item in list)
                {
                    tempList.Add(new { item = item, itemType = EnumExtension.GetItemTypeName(item.ItemType) });
                }
                return Json(tempList, JsonRequestBehavior.AllowGet);
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

        public ActionResult Edit(long id)
        {
            try
            {
                dynamic viewModel = new ExpandoObject();
                Array arr = Enum.GetValues(typeof(MonitorItemType));
                viewModel.item = _business.Get(id);
                viewModel.TypeValue = (int)viewModel.item.ItemType;
                viewModel.TypeName = EnumExtension.GetItemTypeName(viewModel.item.ItemType);
                viewModel.TypePage = viewModel.item.ItemType.ToString() + "Paramter";
                //viewModel.paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject(viewModel.item.Paramter);

                return View(viewModel);
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

        [HttpPost]
        public ActionResult Edit(BaseParamter Model)
        {
            try
            {
                MonitorItemDBModel model = new MonitorItemDBModel();
                model.Id = Convert.ToInt32(Request.Form["Id"]);
                model.Name = Request.Form["Name"].ToString().Trim();
                model.Note = Request.Form["Note"].ToString().Trim();
                model.Host = Request.Form["MHost"].ToString().Trim();
                model.CollectorHost = Request.Form["CollectorHost"].ToString().Trim();
                model.ItemType = (MonitorItemType)int.Parse(Request.Form["ItemType"]);
                model.Paramter = GetParamterStr(Request, model.ItemType);
                bool b = _business.Edit(model);
                return Json(new { State = b });
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
        public ActionResult Delete(long id)
        {
            try
            {
                bool b = _business.Delete(id);
                return Json(new { State = b, Message = "删除成功!" });
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

        private string GetParamterStr(HttpRequestBase request, MonitorItemType type)
        {
            BaseParamter paramter = null;
            switch (type)
            {
                case MonitorItemType.DB:
                    paramter = new Common.Model.DBParamter()
                    {
                        Username = request.Form["Username"].ToString().Trim(),
                        Password = request.Form["Password"].ToString().Trim(),
                        Port = Convert.ToInt32(request.Form["Port"]),
                        Host = request.Form["Host"].ToString().Trim(),
                        DBName = request.Form["DBName"].ToString().Trim(),
                        Interval = Convert.ToInt32(request.Form["Interval"])


                        //DeviceId = Convert.ToInt32(request.Form["DeviceId"])
                    };
                    break;
                case MonitorItemType.DEMS:
                    paramter = new Common.Model.DEMSParamter()
                    {
                        Host = request.Form["Host"].ToString().Trim(),
                        Interval = Convert.ToInt32(request.Form["Interval"]),
                        //DeviceId = Convert.ToInt32(request.Form["DeviceId"]),
                        DEMSName = request.Form["DEMSName"].ToString().Trim()
                    };
                    break;
                case MonitorItemType.Web:
                    paramter = new Common.Model.WebParamter()
                    {
                        Host = request.Form["Host"].ToString().Trim(),
                        Interval = Convert.ToInt32(request.Form["Interval"]),
                        //DeviceId = Convert.ToInt32(request.Form["DeviceId"]),
                        Port = Convert.ToInt32(request.Form["Port"])                        
                    };
                    break;
                default:
                    break;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(paramter);
        }

        public PartialViewResult DBParamter()
        {
            try
            {
                if (RouteData.Values["dbModel"] != null)
                {
                //string dbModel = ViewContext.RouteData.Values["dbModel"].ToString();
                string temp1 = RouteData.Values["dbModel"].ToString();
                object dbModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DBParamter>(temp1);
                return PartialView(dbModel);
            }
                else
                {
                    return PartialView();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartialViewResult WebParamter()
        {
            try
            {
                if (RouteData.Values["dbModel"] != null)
                {
                    //string dbModel = ViewContext.RouteData.Values["dbModel"].ToString();
                    string temp1 = RouteData.Values["dbModel"].ToString();
                    object dbModel = Newtonsoft.Json.JsonConvert.DeserializeObject<WebParamter>(temp1);
                    return PartialView(dbModel);
                }
                else
                {
                    return PartialView();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartialViewResult DEMSParamter()
        {
            try
            {
                if (RouteData.Values["dbModel"] != null)
                {
                    //string dbModel = ViewContext.RouteData.Values["dbModel"].ToString();
                    string temp1 = RouteData.Values["dbModel"].ToString();
                    object dbModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DEMSParamter>(temp1);
                    return PartialView(dbModel);
                }
                else
            {
                    return PartialView();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}