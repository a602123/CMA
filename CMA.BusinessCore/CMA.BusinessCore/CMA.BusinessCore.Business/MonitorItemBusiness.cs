using CMA.BusinessCore.CollectorClient;
using CMA.BusinessCore.DBClient;
using CMA.Common.Model;
using CMA.Common.RedisHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace CMA.BusinessCore.Business
{
    public class MonitorItemBusiness
    {
        private MonitorItemCollectorClient _collectorClient;
        private static MonitorItemBusiness _instance;

        public static MonitorItemBusiness GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MonitorItemBusiness();
            }
            return _instance;
        }

        private MonitorItemBusiness()
        {
            _itemList = new Dictionary<long, BaseMonitorItemModel>();
            _collectorClient = new MonitorItemCollectorClient();
        }

        private Dictionary<long, BaseMonitorItemModel> _itemList;

        public void Init()
        {
            _itemList.Clear();
            IEnumerable<MonitorItemDBModel> itemlist = new MonitorItemDBClient().GetList();
            foreach (var item in itemlist)
            {
                switch (item.ItemType)
                {
                    case MonitorItemType.DEMS:
                        break;
                    case MonitorItemType.Web:
                        var paramter = DeserializeObject<DBParamter>(item.Paramter);
                        if (_collectorClient.AddMonitorItem(item.CollectorHost, paramter))
                        {
                            _itemList.Add(item.Id, new DBMonitorItemModel()
                            {
                                CollectorHost = item.CollectorHost,
                                Id = item.Id,
                                Name = item.Name,
                                Note = item.Note,
                                Paramter = paramter
                            });
                        }
                        break;

                    case MonitorItemType.DB:
                        var paramter2 = DeserializeObject<DBParamter>(item.Paramter);
                        if (_collectorClient.AddMonitorItem(item.CollectorHost, paramter2))
                        {
                            _itemList.Add(item.Id, new DBMonitorItemModel()
                            {
                                CollectorHost = item.CollectorHost,
                                Id = item.Id,
                                Name = item.Name,
                                Note = item.Note,
                                Paramter = paramter2
                            });
                        }
                        break;
                    default:
                        break;
                }
            }

        }




        public bool EditMonitorItem(MonitorItemDBModel model)
        {
            return _collectorClient.AddMonitorItem(model.CollectorHost, null);
        }

        public bool DeleteMonitorItem(MonitorItemDBModel model)
        {
            return _collectorClient.RemoveMonitorItem(model.CollectorHost,model.Id);
        }

        public bool AddMonitorItem(MonitorItemDBModel model)
        {
            return _collectorClient.AddMonitorItem(model.CollectorHost,null);
        }
    }
}
