using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMA.Common.Model
{
    /// <summary>
    /// zhanglianghui
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// 消息编号
        /// </summary>
       
        public decimal Id { get; set; }
        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 所属设备
        /// </summary>
         [DisplayName("所属设备")]
         [Required(ErrorMessage = "请输入所属设备")]
        public int Device { get; set; }
        /// <summary>
        /// 消息值
        /// </summary>
        [DisplayName("消息值")]
        [Required(ErrorMessage = "消息值")]
        public string Value { get; set; }
        /// <summary>
        /// 消息值类型
        /// </summary>
        public MessageValueType ValueType { get; set; }
        /// <summary>
        /// 预留string字段
        /// </summary>
        public string cFree1 { get; set; }
        /// <summary>
        /// 预留bool字段
        /// </summary>
        public Nullable<bool> bFree1 { get; set; }
        /// <summary>
        /// 预留int字段
        /// </summary>

        public Nullable<int> iFree1 { get; set; }
        /// <summary>
        /// 预留float字段
        /// </summary>
        public Nullable<float> fFree1 { get; set; }

    }
}
