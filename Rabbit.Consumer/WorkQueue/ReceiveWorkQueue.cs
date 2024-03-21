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

            //prefetchSize，消息本身的大小 如果设置为0 那么表示对消息本身的大小不限制
            //prefetchCount，告诉rabbitmq不要一次性给消费者推送大于N个消息
            //global，是否将上面的设置应用于整个通道
            //false：表示只应用于当前消费者
            //true：表示当前通道的所有消费者都应用这个限流策略
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: true);

            consumer.Received += (m, e) =>
            {
                string msg = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(100);//睡100ms

                channel.BasicAck(e.DeliveryTag, multiple: false);//手动签收
                Console.WriteLine("消费成功:" + msg);
            };

            channel.BasicConsume(qName, false, consumer);
        }
    }
}
