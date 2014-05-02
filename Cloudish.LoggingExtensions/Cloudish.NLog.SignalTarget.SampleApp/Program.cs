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
        public static Logger Log { get; private set; }

        static void Main(string[] args)
        {            
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("SignalTarget", typeof(Cloudish.NLog.SignalTarget.SignalTarget));
            Log = LogManager.GetCurrentClassLogger(typeof(SignalTarget));

            Log.Error("My first SignalTarget");

            Console.ReadLine();
        }
    }
}
