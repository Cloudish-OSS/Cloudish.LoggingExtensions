using System.Threading;
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
            var rnd = new Random((int) DateTime.Now.Ticks);

            for (int i = 0; i < 100; i++)
            {
                log.Debug("Sample debug message from Log4Net");
                log.Info("Sample informational message from Log4Net");
                log.Warn("Sample warning message from Log4Net");
                log.Error("Sample error message from Log4Net", new Exception("Something bad happened"));
                log.Fatal("Sample fatal error message from Log4Net");

                var sleep = rnd.Next(20, 250);
                Console.WriteLine(string.Concat("Sleeping...:", sleep, "ms"));
                Thread.Sleep(sleep);
            }

            Console.WriteLine("Logging Complete. Press enter to continue...");
            Console.ReadLine();
        }
    }
}
