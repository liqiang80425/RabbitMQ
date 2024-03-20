using Rabbit.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Consumer.Dead
{
    internal class ReceiveDead
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        public static void ReceiveDeadMsg()
        {
            var conn = RabbitMQHelper.GetConnect();
            var channel = conn.CreateModel();
            #region
            //string normalExName = "NormalExchange";//正常交换机名称
            //string normalQueName = "NormalQue";//正常队列名称
            //string normalKey = "NormalKey";//正常路由名称

            //string deadExName = "DeadExchange";//死信交换机名称
            //string deadQueName = "DeadQue";//死信队列名称
            //string deadKey = "DeadKey";//死信队列名称

            ////声明正常交换机
            //channel.ExchangeDeclare(exchange: normalExName, type: "direct", false);
            ////声明正常队列
            //Dictionary<string, object> para = new Dictionary<string, object>();
            //para.Add("x-dead-letter-exchange", deadExName);//死信交换机名称
            //para.Add("x-dead-letter-routing-key", deadKey);//死信路由名称
            //channel.QueueDeclare(normalQueName, false, false, false, para);

            ////绑定正常队列
            //channel.QueueBind(normalQueName, normalExName, normalKey, null);


            ////声明死信交换机
            //channel.ExchangeDeclare(exchange: deadExName, type: "direct", false);
            ////声明死信队列
            //channel.QueueDeclare(deadQueName, false, false, false, null);
            ////绑定死信队列
            //channel.QueueBind(deadQueName, deadExName, deadKey, null);
            #endregion

            var consumer = new EventingBasicConsumer(channel);//消费者
            consumer.Received += (m, e) =>
            {
                string msg = Encoding.UTF8.GetString(e.Body.ToArray());
                channel.BasicReject(e.DeliveryTag, requeue: true);
                Console.WriteLine("已经消费了消息:" + msg);
            };

            channel.BasicConsume("NormalQue", false, consumer);

        }
    }
}
