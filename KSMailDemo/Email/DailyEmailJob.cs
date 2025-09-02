using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace MailDemo.Email
{
    public class DailyEmailJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("Email job started at " + DateTime.Now);

                SendingEmail.SendEmail();

                Console.WriteLine("Email job completed at " + DateTime.Now);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Task.CompletedTask;
        }
    }
}
