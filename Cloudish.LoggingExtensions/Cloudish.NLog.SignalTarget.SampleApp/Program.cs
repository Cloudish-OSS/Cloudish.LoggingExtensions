using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudish.NLog.SignalTarget.SampleApp
{
    class Program
    {
        public static Logger logger { get; private set; }

        static void Main(string[] args)
        {            
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("SignalTarget", typeof(Cloudish.NLog.SignalTarget.SignalTarget));
            logger = LogManager.GetCurrentClassLogger(typeof(SignalTarget));

            logger.Trace("Sample trace message");
            logger.Debug("Sample debug message");
            logger.Info("Sample informational message");
            logger.Warn("Sample warning message");
            logger.Error("Sample error message");
            logger.Fatal("Sample fatal error message");

            Console.ReadLine();
        }
    }
}
