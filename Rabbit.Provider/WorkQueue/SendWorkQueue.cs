using Rabbit.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Provider.WorkQueue
{
    /// <summary>
    /// 工作队列
    /// </summary>
    internal class SendWorkQueue
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        public static void SendWorkQueueMsg()
        {
            string qName = "WorkQueue";//队列名称
            //创建连接
            using (var conn = RabbitMQHelper.GetConnect())
            {
                //创建信道
                using (var channel = conn.CreateModel())
                {
                    //创建队列
                    channel.QueueDeclare(qName, false, false, false, null);

                    int i = 0;
                    while (i<500)
                    {
                        string content = $"工作队列，这是第{i}条";
                        byte[] body = Encoding.UTF8.GetBytes(content);
                        channel.BasicPublish(exchange: "", routingKey: qName, null, body);
                        Console.WriteLine($"发送第{i}条完毕");
                        i++;
                    }
                }
            }

        }
    }
}
