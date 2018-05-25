using CMA.Common.Model;
using CMA.WebSite.DBClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.Business
{
  public   class MessageBusiness
    {
        private MessageDBClient _dbCleint = new MessageDBClient();
        public object GetList()
        {
            return _dbCleint.GetList();
        }
        public void Add(MessageModel model)
        {
            bool result = _dbCleint.Add(model);
            if (result == false)
            {
                throw new AppException("添加失败！");
            }
        }
        public void Delete(int id)
        {
            bool result = _dbCleint.Delete(id);
            if (result == false)
            {
                throw new AppException("删除失败！");

            }
        }
        public void Edit(MessageModel model)
        {
            bool result = _dbCleint.Edit(model);
            if (result == false)
            {
                throw new AppException("修改失败！");
            }
        }
        public MessageModel Get(int  id)
        {
            var model = _dbCleint.Get(id);
            if (model == null)
            {
                throw new AppException("消息不存在");
            }
            else
            {
                return model;
            }
        }
    }
}
