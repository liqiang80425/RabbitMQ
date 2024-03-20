using Rabbit.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Provider.PubSub
{
    internal class SendPubSubQueue
    {
        public static void SendPubSubMsg()
        {
            using (var connection = RabbitMQHelper.GetConnect())
            {
                using (var channel = connection.CreateModel())
                {
                    // 声明交换机对象
                    string exchangeName = "PubSubExchange";//交换机名称
                    channel.ExchangeDeclare(exchangeName, "fanout");//扇形输出
                    // 创建队列
                    string queueName1 = "PubSubQueue1";
                    channel.QueueDeclare(queueName1, false, false, false, null);
                    string queueName2 = "PubSubQueue2";
                    channel.QueueDeclare(queueName2, false, false, false, null);
                    string queueName3 = "PubSubQueue3";
                    channel.QueueDeclare(queueName3, false, false, false, null);
                    // 绑定到交互机
                    // PubSubExchange 绑定了 3个队列 
                    channel.QueueBind(queue: queueName1, exchange: exchangeName, routingKey: "");
                    channel.QueueBind(queue: queueName2, exchange: exchangeName, routingKey: "");
                    channel.QueueBind(queue: queueName3, exchange: exchangeName, routingKey: "");

                    int i = 0;
                    while (i<10)
                    {
                        string message = $"RabbitMQ 第{i}条PubSub消息";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchangeName, "", null, body);
                        Console.WriteLine($"第{i}条PubSub消息发送成功");
                        i++;
                    }
                }
            }

        }
    }
}
