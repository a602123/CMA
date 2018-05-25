using CMA.Common.Model;
using CMA.DataProvider.DataOperator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.DataProvider.Business
{
   public  class MessageDBBusiness
    {
        public IEnumerable<MessageModel> GetList()
        {
            using (var context = new db_cmaEntities())
            {
                DOBase<tb_message> tRole = new DOBase<tb_message>(context);
                return tRole.Where(r => true).ToList().Select(n => GetModel(n));
            }
        }
        public MessageModel Get(long id)
        {
            MessageModel result = null;
            using (var context = new db_cmaEntities())
            {
                DOBase<tb_message> tRole = new DOBase<tb_message>(context);
                result = GetModel(tRole.GetModel(id));
            }
            return result;
        }
        public bool Delete(int id)
        {
            var temp = false;
            using (var context = new db_cmaEntities())
            {
                DOBase<tb_message> tRole = new DOBase<tb_message>(context);
                tRole.Delete(id);

                temp = tRole.SaveChang() > 0 ? true : false;


            }
            return temp;
        }
        public bool Edit(MessageModel model)
        {
            var temp = false;
            using (var context = new db_cmaEntities())
            {
                DOBase<tb_message> tRole = new DOBase<tb_message>(context);
                tRole.Edit(GetModel(model),new string[]{ "Value", "Time" });
               
                temp = tRole.SaveChang() > 0 ? true : false;
                //context.Configuration.ValidateOnSaveEnabled = true;

            }
            return temp;
        }
        public bool Add(MessageModel model)
        {
            var temp = false;
            using (var context = new db_cmaEntities())
            {
                DOBase<tb_message> tRole = new DOBase<tb_message>(context);
                tRole.Add(GetModel(model));
                context.Configuration.ValidateOnSaveEnabled = false;
                temp = tRole.SaveChang() > 0 ? true : false;
                context.Configuration.ValidateOnSaveEnabled = true;

            }
            return temp;
        }
        private MessageModel GetModel(tb_message dbModel)
        {
            return new MessageModel()
            {
                Id = dbModel.Id,
                 Value=dbModel.Value,
                 //ValueType=dbModel.ValueType
            };
        }
        private tb_message GetModel(MessageModel dbModel)
        {
            return new tb_message()
            {
                Id = dbModel.Id,
                Value = dbModel.Value,
                //ValueType = dbModel.ValueType,
                Time = DateTime.Now,
            };
        }
    }
}
