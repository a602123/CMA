using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMA.Common.Model;
using CMA.WebSite.DBClient;
using CMA.Common.EncryptionHelper;

namespace CMA.WebSite.Business
{
    public  class LoginBusiness
    {
        private UserDBClient _dbCleint = new UserDBClient();


        public void Login(UserInfoModel model)
        {
            string md5 = Encryption.GetInstance().MD5Encrypt(model.Password);//900150983CD24FB0D6963F7D28E17F72
            var userInfo = _dbCleint.Login(model);
            if (userInfo == null)
            {
                throw new AppException("用户名不存在！");
            }
            if (md5 != userInfo.Password)
            {
                throw new AppException("密码错误！");
            }
        }
    }
}
