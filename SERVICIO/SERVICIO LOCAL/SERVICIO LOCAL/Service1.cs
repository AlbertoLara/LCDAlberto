using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SERVICIO_LOCAL
{
    public partial class Service1 : ServiceBase
    {
        Timer tmService = null;
        localhost.WebService1 objService;
        public Service1()
        {
            InitializeComponent();
            objService = new localhost.WebService1();
            tmService = new Timer(1000);
            tmService.Elapsed += new ElapsedEventHandler(tmServicio_Elapsed);
        }
        public void tmServicio_Elapsed(object sender, ElapsedEventArgs e)
        {
            string hora = "11:09 p.m.";
            if (DateTime.Now.ToShortTimeString() == hora)
            {
                try
                {
                    tmService.Stop();
                    objService.UpdateLCO();
                }
                catch{ }
                tmService.Start();
            }
        }
        
        protected override void OnStart(string[] args)
        {
            tmService.Start();
        }
        protected override void OnStop()
        {
        }
    }
}
