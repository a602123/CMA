using CMA.Common.Model;
using CMA.WebSite.DBClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.Business
{
    public class ActionBusiness
    {
        private ActionDBClient _client = new ActionDBClient();
        public IEnumerable<ActionModel> GetList()
        {
            return _client.GetList();
        }
    }
}
