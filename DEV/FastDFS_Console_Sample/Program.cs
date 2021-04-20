using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using FastDFS.Client;

namespace FastDFS_Console_Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // fastdfs同步有延时,如果访问到还未同步的节点,则会报错

            List<IPEndPoint> pEndPoints = new List<IPEndPoint>()
            {
                new IPEndPoint(IPAddress.Parse("10.4.52.14"), 22122),
                new IPEndPoint(IPAddress.Parse("10.4.52.17"), 22122),
                //new IPEndPoint(IPAddress.Parse("192.168.199.133"), 22122),
            };
            ConnectionManager.Initialize(pEndPoints);
            //await Demo1.RunAsync();
            //await Demo2.RunAsync();
            await Demo2.AddFileAsync();
        }

    }
}
