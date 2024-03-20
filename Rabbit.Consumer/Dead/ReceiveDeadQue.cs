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
    internal class ReceiveDeadQue
    {
        public static void ReceiveDeadQueMsg()
        {
            var conn = RabbitMQHelper.GetConnect();
            var channel = conn.CreateModel();

            string deadExName = "DeadExchange";
            string deadQueName = "DeadQue";
            string deadKey = "DeadKey";//死信队列名称

            //声明死信交换机
            channel.ExchangeDeclare(exchange: deadExName, type: "direct", false);
            //声明死信队列
            channel.QueueDeclare(deadQueName, false, false, false, null);
            //绑定死信队列
            channel.QueueBind(deadQueName, deadExName, deadKey, null);


            var consumer = new EventingBasicConsumer(channel);//消费者
            consumer.Received += (m, e) =>
            {
                string msg = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("已经消费了死信新列的消息:" + msg);
            };

            channel.BasicConsume("DeadQue", true, consumer);

        }
    }
}
