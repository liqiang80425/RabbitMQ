using Rabbit.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Consumer.Helloword
{
    /// <summary>
    /// hello word接收
    /// </summary>
    internal class ReceiveHelloword
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        public static void ReceiveHellowordMsg()
        {
            string qName = "HwQue";//队列名称
                                   //创建连接
            var conn = RabbitMQHelper.GetConnect();

            var channel = conn.CreateModel();
            //创建队列 如果先启动消费者，且又不创建队列，程序异常
            channel.QueueDeclare(qName, false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);//消费者
            consumer.Received += (m, e) =>
            {
                string msg = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("已经消费了消息:" + msg);
            };

            channel.BasicConsume(qName, true, consumer);

        }
    }
}
