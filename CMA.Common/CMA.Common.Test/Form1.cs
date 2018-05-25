using CMA.Common.Model;
using CMA.Common.RedisHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMA.Common.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            UserInfoModel[] users =
            {
                new UserInfoModel()
                {
                    Id = 0,
                    Email = "lingchen0617@.sina.com",
                    Password = "qwe123!@#",
                    RealName = "凌晨",
                    Telphone = "17737618051",
                    Username = "lingchen"
                },
                new UserInfoModel()
                {
                    Id = 1,
                    Email = "lingchen0618@.sina.com",
                    Password = "qwe123!@#1",
                    RealName = "凌晨1",
                    Telphone = "17737618052",
                    Username = "lingchen1"
                },new UserInfoModel()
                {
                    Id = 2,
                    Email = "lingchen0619@.sina.com",
                    Password = "qwe123!@#2",
                    RealName = "凌晨2",
                    Telphone = "17737618053",
                    Username = "lingchen2"
                },new UserInfoModel()
                {
                    Id = 3,
                    Email = "lingchen0620@.sina.com",
                    Password = "qwe123!@#3",
                    RealName = "凌晨3",
                    Telphone = "17737618054",
                    Username = "lingchen3"
                }
            };

            var redis = RedisOperator.GetInstance();

            redis.RPush<UserInfoModel>("lingchen", users);

            MessageBox.Show((DateTime.Now - now).TotalMilliseconds.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var redis = RedisOperator.GetInstance();
            var item = redis.LPop<UserInfoModel>("lingchen");
            if (item != null)
            {

                MessageBox.Show($"Name:{item.RealName}");
            }
            else
            {
                MessageBox.Show("null");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DBMonitorItemModel item = new DBMonitorItemModel()
            {
                Id = 1,
                Name = "123",
                Note = "223",
                Paramter = new DBParamter()
                {
                    DBName = "sql",
                    Host = "192.168.1.1",
                    Interval = 10,
                    Password = "1234",
                    Port = 123,
                    Username = "sa"
                }

            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(item.GetPatamter());
            Show(item);

            MessageBox.Show(item.GetPatamter().Password);
        }

        private void Show(BaseMonitorItemModel model)
        {
            string name = model.Name;
            string type = model.ItemType.GetItemTypeName();
            string interval = model.Paramter.Interval.ToString();

            MessageBox.Show($"{model.Name}\r{model.ItemType.GetItemTypeName()}\r{model.Paramter.Interval}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var redis = RedisOperator.GetInstance();

            Dictionary<string, MonitorItemModel>  list =redis.HashGetAll<MonitorItemModel>("010002FF-00AC-AC02-0003-0000FFFF0007");
            //MonitorItemModel item = redis.HashGet<MonitorItemModel>("010002FF-00AC-AC02-0003-0000FFFF0007", Newtonsoft.Json.JsonConvert.SerializeObject("回风温度"));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var redis = RedisOperator.GetInstance();
            bool b = redis.HashSet<string>("zxm", "zxm", "zhengxumei");
            bool b1 = redis.HashSet<string>("zxm", "zxm1", "zhengxumei1");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var redis = RedisOperator.GetInstance();
            Dictionary<string, string> list = redis.HashGetAll<string>("zxm");
            //string list = redis.HashGet<string>("zxm","zxm");

        }
    }



    public class MonitorItemModel
    {
        public int ID { get; set; }
        public int IsAler { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
    }
}
