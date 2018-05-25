using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.Model
{
    [Serializable]
    public class DBParamter: BaseParamter
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string DBName { get; set; }
    }
}
