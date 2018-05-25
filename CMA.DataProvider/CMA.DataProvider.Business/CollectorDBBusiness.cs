using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMA.Common.Model;
using CMA.DataProvider.DataOperator;

namespace CMA.DataProvider.Business
{
    public class CollectorDBBusiness
    {
        public IEnumerable<CollectorDBModel> GetList()
        {
            using (var context = new db_cmaEntities())
            {
                var tbItemList = context.tb_collector.ToList();
                IEnumerable<CollectorDBModel> itemList = tbItemList.Select(n => GetDBCollectorModel(n));
                return itemList;
            }
        }

        public bool Add(CollectorDBModel model)
        {
            bool result = false;
            using (var context = new db_cmaEntities())
            {
                context.tb_collector.Add(new tb_collector() { Host = model.Host, Note = model.Note });
                result = context.SaveChanges() > 0 ? true : false;
            }
            return result;
        }


        public bool Delete(long id)
        {
            bool result = false;
            try
            {
                using (var context = new db_cmaEntities())
                {
                    var dbmodel = context.tb_collector.First(u => u.Id == id);
                    if (dbmodel != null)
                    {
                        context.tb_collector.Remove(dbmodel);
                        result = context.SaveChanges() > 0?true:false;
                    }
                    else
                    {
                        result = true;//已删除
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private CollectorDBModel GetDBCollectorModel(tb_collector dbModel)
        {
            return new CollectorDBModel()
            {
                Id = dbModel.Id,
                Host = dbModel.Host,
                Note = dbModel.Note
            };
        }
    }
}
