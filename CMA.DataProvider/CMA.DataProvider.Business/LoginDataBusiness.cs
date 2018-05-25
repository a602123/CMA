using CMA.DataProvider.DataOperator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.DataProvider.Business
{
    public class LoginDataBusiness
    {
        public bool Login(tb_user user)
        {
            using (var context = new db_cmaEntities())
            {
                return context.tb_user.Where(n => n.Username == user.Username && n.Password == user.Password).FirstOrDefault() != null;
            }
        }
    }
}
