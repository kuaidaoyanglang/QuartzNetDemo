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
                /*
                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloWordJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(1)
                        .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);
                // some sleep to show what's happening
                //await Task.Delay(TimeSpan.FromSeconds(10));

                // and last shut down the scheduler when you are ready to close your program
                //await scheduler.Shutdown();
                */
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
    }
}
