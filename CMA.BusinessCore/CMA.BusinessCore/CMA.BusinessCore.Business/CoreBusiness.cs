using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.BusinessCore.Business
{
    public class CoreBusiness
    {
        private bool _coreState = false;
        private int _coreMinInterval = 0;

        #region 时间记录
        private DateTime _collectorCheckStateTime = DateTime.Now;
        //private DateTime _collectorCheckStateTime = DateTime.Now;
        #endregion

        public void Initialize()
        {
            _coreState = false;

            #region 初始化

            #endregion


            System.Timers.Timer timer = new System.Timers.Timer(1500);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //这里判断


            
        }

        private void CommandSender_CollectorCheckState(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }


    public class CoreCommandSender : IDisposable
    {
        #region Business
        private CollectorBusiness _collectorBusiness;
        private MessageBusiness _messageBusiness;
        private MonitorItemBusiness _monitorItemBusiness;
        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public event EventHandler CollectorCheckState;

        public CoreCommandSender()
        {
            #region Business初始化
            _collectorBusiness = Business.CollectorBusiness.GetInstance();
            _messageBusiness = MessageBusiness.GetInstance();
            _monitorItemBusiness = MonitorItemBusiness.GetInstance();
            #endregion

            #region 初始化事件
            CollectorCheckState += CoreCommandSender_CollectorCheckState;

            
            #endregion
        }

        public void Start() {

        }

        private void CoreCommandSender_CollectorCheckState(object sender, EventArgs e)
        {
            
        }
    }
}
