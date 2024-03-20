using Rabbit.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Consumer.WorkQueue
{
    internal class ReceiveWorkQueue
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        public static void ReceiveWorkMsg()
        {
            string qName = "WorkQueue";//队列名称
                                   //创建连接
            var conn = RabbitMQHelper.GetConnect();

            var channel = conn.CreateModel();
            //创建队列 如果先启动消费者，且又不创建队列，程序异常
            channel.QueueDeclare(qName, false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);//消费者

            channel.BasicQos(prefetchSize:0,prefetchCount:1,global:true);
            consumer.Received += (m, e) =>
            {
                string msg = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(100);

                Console.WriteLine("接收成功:" + msg);
                channel.BasicAck(e.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(qName, false, consumer);

        }
    }
}
