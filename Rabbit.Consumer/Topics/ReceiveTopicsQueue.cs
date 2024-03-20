using Rabbit.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Consumer.Topics
{
    internal class ReceiveTopicsQueue
    {
        public static void ReceiveTopicsMsg()
        {
            var connection = RabbitMQHelper.GetConnect();
            {
                var channel = connection.CreateModel();
                {
                    //申明exchange
                    string exchangeName = "TopicsExchange";//交换机名称
                    channel.ExchangeDeclare(exchange: exchangeName, type: "topic",durable:true);//主题

                    // 创建队列
                    string queueName1 = "TopicsQueue1";
                    channel.QueueDeclare(queueName1,durable: true, false, false, null);
                    // 绑定到交互机
                    channel.QueueBind(queue: queueName1, exchange: exchangeName, routingKey: "RouteKey.*");

                    //申明consumer
                    var consumer = new EventingBasicConsumer(channel);
                    //绑定消息接收后的事件委托
                    consumer.Received += (m, e) =>
                    {
                        var body = e.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine($"接收的消息{message}", message);
                    };

                    channel.BasicConsume(queue: queueName1, autoAck: true, consumer: consumer);
                }
            }
        }
    }
}
