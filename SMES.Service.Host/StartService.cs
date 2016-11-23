using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Service.Host
{
    public class StartService
    {
        public static void Start()
        {
            Console.Title = "SMES-业务服务器";

            WriteConsole("开始启动业务组件服务器");

            StartLog();
            StartIOC();
            StartParseAssemblys();
            StartDomain();
            StartBusiness();
            

        }

        private static void StartLog()
        {
            WriteConsole("开始配置日志....");
            try
            {
                MDF.Framework.Log.LogBase.Config();
                WriteConsole("配置日志完成");
            }
            catch(Exception ee)
            {
                WriteConsole(ee.ToString());
            }
            
        }

        private static void StartIOC()
        {
            WriteConsole("开始配置IOC....");
            try
            {
                MDF.Framework.Bus.ObjectFactory.Config();
                WriteConsole("IOC配置完成");
            }
            catch (Exception ee)
            {
                WriteConsole(ee.ToString());
            }
            
        }

        private static void StartParseAssemblys()
        {
            WriteConsole("开始配置熟知类型....");
            try
            {
                MDF.Framework.Bus.ObjectFactory.ParseAssemblys();
                WriteConsole("熟知类型配置完成");
            }
            catch(Exception ee) {
                WriteConsole(ee.ToString());
            }
            
            
        }

        private static void CreateTypeProxy()
        {
            WriteConsole("开始配置服务代理缓存....");
            try
            {
                MDF.Framework.Bus.ObjectFactory.CreateTypeProxy();
                WriteConsole("服务代理缓存启动完成");
            }
            catch (Exception ee)
            {
                WriteConsole(ee.ToString());
            }
            
        }

        private static void StartDomain()
        {
            WriteConsole("正在启动跨域访问服务....");

            try
            {
                var crossDomainServiceHost = new ServiceHost(typeof(MDF.Service.CrossDomainService));               
                crossDomainServiceHost.Open();
                WriteConsole("跨域服务启动完成");
            }
            catch (Exception ee)
            {
                WriteConsole(ee.ToString());
            }
        }

        private static void StartBusiness()
        {
            WriteConsole("正在启动业务组件服务....");
            try
            {
                var businessServiceHost = new ServiceHost(typeof(MDF.Service.BusinessService));                
                businessServiceHost.Open();
                WriteConsole("业务组件服务启动完成....");
            }
            catch (Exception ee)
            {
                WriteConsole(ee.ToString());
            }
        }       

        private static void WriteConsole(string msg)
        {
            Console.WriteLine("["+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"]");
            Console.WriteLine(msg);
        }
    }
}
