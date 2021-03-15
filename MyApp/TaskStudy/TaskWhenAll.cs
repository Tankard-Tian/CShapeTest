using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TaskStudy
{
    /// <summary>
    /// 等待一组任务任务完成    
    /// </summary>
    public class TaskWhenAll
    {
        public static async Task DealyTaskAsync()
        {
            Task task1 =  Task.Run(() => Thread.Sleep(1000));
            Task task2 = Task.Factory.StartNew(() => Thread.Sleep(9000));
            await Task.WhenAny(new Task[] { task1, task2 });
        }
    }
}
