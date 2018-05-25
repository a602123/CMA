using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CMA.Common.Model
{
    [Serializable]
    public class UserInfoModel
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        [Required(ErrorMessage="请输入用户名")]
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [Required(ErrorMessage ="请输入密码")]
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [DisplayName("确认密码")]
        [Required(ErrorMessage = "请输入确认密码")]
        [Compare("Password",ErrorMessage ="密码不一致")]
        public string PasswordAgain { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        [DisplayName("联系人")]
        [Required(ErrorMessage ="请输入您联系人姓名")]
        public string RealName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        [Required(ErrorMessage = "请输入您的手机号码")]
        [RegularExpression(@"^1[345678][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        public string Telphone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("电子邮箱")]
        [Required(ErrorMessage = "请输入您的电子邮箱地址")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", 
            ErrorMessage="电子邮箱地址不是有效地址")]
        public string Email { get; set; }


        /// <summary>
        /// 用户状态 0--正常用户  1--已被删除的用户
        /// </summary>
        public Nullable<int> State { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        [Required(ErrorMessage ="请选择用户的角色")]
        public List<long> RoleList { get; set; }
        
    }
}
