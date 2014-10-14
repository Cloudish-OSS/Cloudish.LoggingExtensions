using NLog;
using NLog.Config;
using System;

namespace Cloudish.NLog.SignalTarget.SampleApp
{
    class Program
    {
        public static Logger Logger { get; private set; }

        static void Main(string[] args)
        {            
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("SignalTarget", typeof(SignalTarget));
            Logger = LogManager.GetCurrentClassLogger(typeof(SignalTarget));

            for (int i = 0; i < 100; i++) {
                Logger.Trace("Sample trace message from NLog");
                Logger.Debug("Sample debug message from NLog");
                Logger.Info("Sample informational message from NLog");
                Logger.Warn("Sample warning message from NLog");
                Logger.Error("Sample error message from NLog", new Exception("Something bad happened!"));
                Logger.Fatal("Sample fatal error message from NLog");   
            }

            Console.WriteLine("Logging Complete. Press enter to continue...");
            Console.ReadLine();
        }
    }
}
