using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMA.Common.Model;
using CMA.DataProvider.DataOperator;
using static Newtonsoft.Json.JsonConvert;

namespace CMA.DataProvider.Business
{
    public class MonitorItemBusiness
    {
        public IEnumerable<MonitorItemDBModel> GetList()
        {
            using (var context = new db_cmaEntities())
            {
                var tbItemList = context.tb_monitoritem.ToList();
                IEnumerable<MonitorItemDBModel> itemList = tbItemList.Select(n => GetDBMonitorItemModel(n));
                return itemList;
            }
        }

        public bool Add(MonitorItemDBModel model)
        {
            bool result = false;
            using (var context = new db_cmaEntities())
            {
                context.tb_monitoritem.Add(new tb_monitoritem()
                {
                    Host = model.Host,
                    ItemType = (int)model.ItemType,
                    Name = model.Name,
                    Note = model.Note,
                    CollectorHost = model.CollectorHost,
                    Paramter = model.Paramter
                });

                context.SaveChanges();
                result = true;
            }
            return result;
        }

        public MonitorItemDBModel Get(int id)
        {
            using (var context = new db_cmaEntities())
            {
                var tbItem = context.tb_monitoritem.Where(n => n.Id == id).FirstOrDefault();
                MonitorItemDBModel itemList = GetDBMonitorItemModel(tbItem);
                return itemList;
            }
        }

        public bool Edit(MonitorItemDBModel model)
        {
            bool result = false;
            using (var context = new db_cmaEntities())
            {
                var tbItem = context.tb_monitoritem.Where(n => n.Id == model.Id).FirstOrDefault();

                tbItem.Host = model.Host;
                //tbItem.ItemType = (int)model.ItemType;
                tbItem.Name = model.Name;
                tbItem.Note = model.Note;
                tbItem.Paramter = model.Paramter;
                tbItem.CollectorHost = model.CollectorHost;

                context.SaveChanges();
                result = true;
            }
            return result;
        }

        private MonitorItemDBModel GetDBMonitorItemModel(tb_monitoritem dbModel)
        {
            return new MonitorItemDBModel()
            {
                Host = dbModel.Host,
                Id = dbModel.Id,
                ItemType = (MonitorItemType)dbModel.ItemType,
                Name = dbModel.Name,
                Note = dbModel.Note,
                CollectorHost = dbModel.CollectorHost,
                Paramter = dbModel.Paramter
            };
        }


        public bool Delete(long id)
        {
            bool result = false;
            try
            {
                using (var context = new db_cmaEntities())
                {
                    var dbmodel = context.tb_monitoritem.First(u => u.Id == id);
                    if (dbmodel != null)
                    {
                        context.tb_monitoritem.Remove(dbmodel);
                        result = context.SaveChanges() > 0;
                    }
                    else
                    {
                        return true;//已删除
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
