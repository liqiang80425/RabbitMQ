using Rabbit.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Consumer.Delay
{
    internal class ReceiveDelay
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        public static void ReceiveDelayMsg()
        {
            var conn = RabbitMQHelper.GetConnect();
            var channel = conn.CreateModel();

            var consumer = new EventingBasicConsumer(channel);//消费者
            consumer.Received += (m, e) =>
            {
                string msg = Encoding.UTF8.GetString(e.Body.ToArray());
                channel.BasicReject(e.DeliveryTag, requeue: true);
                Console.WriteLine("已经消费了延时消息:" + msg);
            };

            channel.BasicConsume("delayed_queue", true, consumer);

        }
    }
}
