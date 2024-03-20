using Rabbit.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Provider.Routing
{
    internal class SendRoutingQueue
    {
        /// <summary>
        /// 路由模式
        /// </summary>
        public static void SendRoutingMsg()
        {
            using (var connection = RabbitMQHelper.GetConnect())
            {
                using (var channel = connection.CreateModel())
                {
                    // 声明交换机对象
                    string exchangeName = "RoutingExchange";//交换机名称
                    channel.ExchangeDeclare(exchangeName, "direct");//完全匹配/直接
                    // 创建队列
                    string queueName1 = "RoutingQueue1";
                    channel.QueueDeclare(queueName1, false, false, false, null);
                    string queueName2 = "RoutingQueue2";
                    channel.QueueDeclare(queueName2, false, false, false, null);
                    string queueName3 = "RoutingQueue3";
                    channel.QueueDeclare(queueName3, false, false, false, null);
                    // 绑定到交互机
                    // PubSubExchange 绑定了 3个队列 
                    channel.QueueBind(queue: queueName1, exchange: exchangeName, routingKey: "RouteKey1");
                    channel.QueueBind(queue: queueName2, exchange: exchangeName, routingKey: "RouteKey2");
                    channel.QueueBind(queue: queueName3, exchange: exchangeName, routingKey: "RouteKey3");

                    int i = 0;
                    while (i < 10)
                    {
                        string message = $"RabbitMQ 给RouteKey1发送的第{i}条消息";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchangeName, "RouteKey1", null, body);
                        Console.WriteLine($"给RouteKey1,第{i}条Routing消息发送成功");
                        i++;
                    }
                }
            }

        }
    }
}
