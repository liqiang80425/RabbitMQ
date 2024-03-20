using Rabbit.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Provider.Topics
{
    internal class SendTopicsQueue
    {
        /// <summary>
        /// 主题模式
        /// </summary>
        public static void SendTopicsMsg()
        {
            using (var connection = RabbitMQHelper.GetConnect())
            {
                using (var channel = connection.CreateModel())
                {

                    //1、交换机持久化
                    //2、队列持久化
                    //3、消息持久化

                    // 声明交换机对象
                    string exchangeName = "TopicsExchange";//交换机名称
                    channel.ExchangeDeclare(exchangeName, type: "topic", durable: true);//主题/关键字  durable:交换机持久化
                    // 创建队列
                    string queueName1 = "TopicsQueue1";
                    channel.QueueDeclare(queueName1, durable: true, false, false, null); //2、durable 队列持久化
                    string queueName2 = "TopicsQueue2";
                    channel.QueueDeclare(queueName2, durable: true, false, false, null);
                    string queueName3 = "TopicsQueue3";
                    channel.QueueDeclare(queueName3, durable: true, false, false, null);

                    // 绑定到交互机
                    // PubSubExchange 绑定了 3个队列 
                    channel.QueueBind(queue: queueName1, exchange: exchangeName, routingKey: "RouteKey.#");//*:通配符,一个单词。#：可以一个或多个单词
                    channel.QueueBind(queue: queueName2, exchange: exchangeName, routingKey: "RouteKey2");
                    channel.QueueBind(queue: queueName3, exchange: exchangeName, routingKey: "RouteKey3");

                    //信道创建属性
                    var prop = channel.CreateBasicProperties();
                    prop.Persistent = true;//消息持久化

                    int i = 0;
                    while (i < 10)
                    {
                        string message = $"RabbitMQ topic路由发送的第{i}条消息";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: exchangeName, routingKey: "RouteKey.Add.def", prop, body);
                        Console.WriteLine($"给RouteKey.Add,第{i}条Topic消息发送成功");
                        i++;
                    }
                }
            }

        }
    }
}
