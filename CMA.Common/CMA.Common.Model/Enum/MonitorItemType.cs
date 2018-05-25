using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.Model
{
    public enum MonitorItemType
    {
        [ItemTypeName("数据监控")]
        DB,
        [ItemTypeName("动环监控")]
        DEMS,
        [ItemTypeName("Web监控")]
        Web
    }


    public enum CollectorType
    {

        [ParamterType(typeof(DBParamter))]
        [CollectorType("CMA.CollectCenter,CMA.CollectCenter.DEMSCollector")]
        DEMS,

        [ParamterType(typeof(DBParamter))]
        [CollectorType("Collector1,Collector1.WebCollector")]
        Web
    }

    public class CollectorTypeAttribute : Attribute
    {
        private string _value;
        public CollectorTypeAttribute(string value)
        {
            _value = value;
        }
        /// <summary>
        /// 采集器类型
        /// </summary>
        public string CollectorType
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public class ParamterTypeAttribute : Attribute
    {
        private Type _value;
        public ParamterTypeAttribute(Type value)
        {
            _value = value;
        }
        /// <summary>
        /// 参数类型
        /// </summary>
        public Type ParamterType
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public class ItemTypeNameAttribute : Attribute
    {
        private string _value;
        public ItemTypeNameAttribute(string value)
        {
            _value = value;
        }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string ItemTypeName
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public static class EnumExtension
    {
        public static string GetCollectorType(this CollectorType val)
        {
            Type type = val.GetType();
            FieldInfo fd = type.GetField(val.ToString());
            if (fd == null)
            {
                return string.Empty;
            }
            var attr = fd.GetCustomAttributes(typeof(CollectorTypeAttribute), false).FirstOrDefault() as CollectorTypeAttribute;
            if (attr == null)
            {
                return string.Empty;
            }
            return attr.CollectorType;
        }

        public static Type GetParamterType(this CollectorType val)
        {
            Type type = val.GetType();
            FieldInfo fd = type.GetField(val.ToString());
            if (fd == null)
            {
                return null;
            }
            var attr = fd.GetCustomAttributes(typeof(ParamterTypeAttribute), false).FirstOrDefault() as ParamterTypeAttribute;
            if (attr == null)
            {
                return null;
            }
            return attr.ParamterType;
        }

        public static string GetItemTypeName(this MonitorItemType val)
        {
            Type type = val.GetType();
            FieldInfo fd = type.GetField(val.ToString());
            if (fd == null)
            {
                return string.Empty;
            }
            var attr = fd.GetCustomAttributes(typeof(ItemTypeNameAttribute), false).FirstOrDefault() as ItemTypeNameAttribute;
            if (attr == null)
            {
                return string.Empty;
            }
            return attr.ItemTypeName;
        }


        public static List<string> GetMonitorItemTypeNames()
        {
            Array arr = Enum.GetValues(typeof(MonitorItemType));
            List<string> list = new List<string>();
            foreach (var item in arr)
            {
                list.Add(GetItemTypeName((MonitorItemType)Convert.ToInt32(item)));
            }
            return list;
        }
    }
}
