using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudish.Log4Net.SignalAppender.SampleApp
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            ILog log = log4net.LogManager.GetLogger(typeof(Program));
            
            log.Error("This is a fatal message");
            Console.ReadLine();

        }
    }
}
