using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Net;
using Quartz.Net.Jobs;

namespace ConsoleSample
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Schedule<Message> schedule=new Schedule<Message>("* * * ? * *");
            schedule.ScheduleJob("Gorev1","Gorevler");
            schedule.StartJob();

            Schedule<Message2> schedule1 = new Schedule<Message2>("* * * ? * *");
            schedule1.ScheduleJob("Gorev2", "Gorevler");
            schedule1.StartJob();
            Console.ReadLine();
        }

    

    }

}
