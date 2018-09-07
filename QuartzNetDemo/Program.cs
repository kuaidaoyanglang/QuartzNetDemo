using Quartz;
using Quartz.Impl;
using QuartzNetDemo.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgram().GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static async Task RunProgram()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                //NameValueCollection props = new NameValueCollection
                //{
                //    { "quartz.serializer.type", "binary" }
                //};
                NameValueCollection configuration = (NameValueCollection)ConfigurationManager.GetSection("quartz");
                foreach (var item in configuration.AllKeys)
                {
                    Console.WriteLine($"{item}:{configuration[item]}");
                }
                StdSchedulerFactory factory = new StdSchedulerFactory(configuration);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();
                
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
    }
}
