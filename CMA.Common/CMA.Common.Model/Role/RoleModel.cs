
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMA.Common.Model
{
    public class RoleModel
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [DisplayName("角色名称")]
        [Required(ErrorMessage = "请输入角色名")]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [DisplayName("角色描述")]
        [Required(ErrorMessage = "请输入角色描述")]
        public string Description { get; set; }


        public string cFree { get; set; }

        public long? iFree { get; set; }

        public IEnumerable<long> ActionList { get; set; }
    }
}
