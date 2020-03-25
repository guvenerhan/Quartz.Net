using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz.Core;
using Quartz.Impl;

namespace Quartz.Net
{
    public class Schedule<T> where T : IJob
    {
        private IScheduler _scheduler;
        private string _cronExpression;
        public Schedule(string cronExpression)
        {
            _cronExpression = cronExpression;
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
        }
        public async void ScheduleJob(string name,string group)
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            _scheduler = await factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(name, group)
                .StartNow()
                .WithCronSchedule(_cronExpression)
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
            
        }

        public async void StartJob()
        {
            await _scheduler.Start();
        }
        public void StopJob()
        {
            _scheduler.Shutdown();
        }

    }
}
