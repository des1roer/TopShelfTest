using System;
//----------
using System.Timers;
using Topshelf;
using NLog;

namespace TopShelfTest
{
    public class TownCrier
    {
        private static Logger logger = LogManager.GetCurrentClassLogger(); //log
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);

        }
        public void Start()
        {
            _timer.Start();
            logger.Trace("Start at {0}", DateTime.Now);
        }
        public void Stop()
        {
            _timer.Stop();
            logger.Debug("Stop at {0}", DateTime.Now);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {

                x.Service<TownCrier>(s =>
                {
                    s.ConstructUsing(name => new TownCrier());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Sample Topshelf Host");
                x.SetDisplayName("Stuff");
                x.SetServiceName("Stuff");
            });
        }
    }
}
