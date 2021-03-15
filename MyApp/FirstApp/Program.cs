using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        static async Task<T> DelayResult<T>(T result, TimeSpan delay)
        {
            await Task.Delay(delay);
            return result;
        }


        static async Task<string> DownloadStringWithRetries(string url)
        {
            using (var client = new HttpClient())
            {
                var nextDelay = TimeSpan.FromSeconds(1);
                for (int i = 0; i != 3; ++i)
                {
                    try
                    {
                        return await client.GetStringAsync(url);
                    }
                    catch
                    {
                    }
                    await Task.Delay(nextDelay);
                    nextDelay += nextDelay;
                }
                return await client.GetStringAsync(url);
            }
        }

        // 3秒内没有响应 返回null
        static async Task<string> DownloadStringWithTimeout(string url)
        {
            using (var client = new HttpClient())
            {
                Task<string> downloadTask = client.GetStringAsync(url);
                Task timeoutTask = Task.Delay(3000);
                var completedTask = await Task.WhenAny(downloadTask, timeoutTask);
                if(completedTask == timeoutTask)
                {
                    return null;
                }
                return await downloadTask;
            }
        }
    }
}
