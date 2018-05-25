using CMA.Common.Model;
using CMA.DataProvider.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CMA.DataProvider.Service
{
    public class UserController:ApiController
    {
        private UserDBBusiness _business = new UserDBBusiness();
        [HttpGet]
        public UserInfoModel Get(string username)
        {
            try
            {
                return _business.Get(username);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }
        [HttpGet]
        public UserInfoModel Get(long id)
        {
            try
            {
                return _business.Get(id);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns>"1"--添加成功</returns>
        /// <returns>"0"--添加失败</returns>
        /// <returns>"-1"--添加失败</returns>
        public int Add(UserInfoModel model)
        {            
            try
            {
                if (!_business.IsExit(model))
                {
                    return _business.Add(model)?(int) AddUserError.Success:(int) AddUserError.Error;
                }
                else
                {
                    return (int) AddUserError.UserNameExits;
                }                
            }            
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }

        [HttpGet]        
        public bool Delete(long id)
        {
            try
            {
                return _business.Delete(id);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }

        [HttpPost]
        public bool Edit(UserInfoModel model)
        {
            try
            {
                return _business.Edit(model);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }

        public bool RestPassword(UserInfoModel model)
        {
            try
            {
                return _business.RestPassword(model);
            }
            catch (Exception ex)
            {

                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }

        [HttpGet]
        public IEnumerable<UserInfoModel> GetList()
        {
            try
            {
                return _business.GetList();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }
    }
}
