// See https://aka.ms/new-console-template for more information
using MailDemo.Email;
using Quartz;
using Quartz.Impl;
namespace MailDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Quartz.NET job scheduler...");

            try
            {
                // 1. Create scheduler
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();

                // 2. Define job
                IJobDetail job = JobBuilder.Create<DailyEmailJob>()
                    .WithIdentity("dailyEmailJob", "emailGroup")
                    .Build();

                // 3. Define trigger - every 1 minute
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("minuteTrigger", "emailGroup")
                    .WithCronSchedule("0 0/1 * * * ?") // every 1 min
                    .Build();

                // 4. Schedule job
                await scheduler.ScheduleJob(job, trigger);

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

                // 5. Graceful shutdown
                await scheduler.Shutdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Scheduler error: " + ex.Message);
            }
        }
    }
}
