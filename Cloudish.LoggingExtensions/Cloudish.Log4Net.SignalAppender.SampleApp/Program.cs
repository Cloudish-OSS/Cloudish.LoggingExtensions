using log4net;
using System;

namespace Cloudish.Log4Net.SignalAppender.SampleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            ILog log = LogManager.GetLogger(typeof(Program));

            log.Debug("Sample debug message from Log4Net");
            log.Info("Sample informational message from Log4Net");
            log.Warn("Sample warning message from Log4Net");
            log.Error("Sample error message from Log4Net");
            log.Fatal("Sample fatal error message from Log4Net");
            
            Console.ReadLine();
        }
    }
}
