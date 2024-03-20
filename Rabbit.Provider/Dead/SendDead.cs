using Rabbit.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Provider.Dead
{
    /// <summary>
    /// 死信(有效期)
    /// </summary>
    internal class SendDead
    {
        /// <summary>
        /// 发送死信 (有效期)
        /// </summary>
        public static void SendDeadMsg()
        {
            using (var connection = RabbitMQHelper.GetConnect())
            {
                using (var channel = connection.CreateModel())
                {
                    string normalExName = "NormalExchange";//正常交换机名称
                    string normalQueName = "NormalQue";//正常队列名称
                    string normalKey = "NormalKey";//正常路由名称

                    string deadExName = "DeadExchange";//死信交换机名称
                    string deadQueName = "DeadQue";//死信队列名称
                    string deadKey = "DeadKey";//死信队列名称

                    //声明正常交换机
                    channel.ExchangeDeclare(exchange: normalExName, type:"direct", false);
                    //声明正常队列
                    Dictionary<string, object> para = new Dictionary<string, object>();
                    para.Add("x-dead-letter-exchange", deadExName);//死信交换机名称
                    para.Add("x-dead-letter-routing-key", deadKey);//死信路由名称
                    //para.Add("x-max-length",10);//最多10条
                    channel.QueueDeclare(normalQueName,false,false,false, para);

                    //绑定正常队列
                    channel.QueueBind(normalQueName, normalExName, normalKey,null);


                    //声明死信交换机
                    channel.ExchangeDeclare(exchange: deadExName, type: "direct", false);
                    //声明死信队列
                    channel.QueueDeclare(deadQueName, false, false, false, null);
                    //绑定死信队列
                    channel.QueueBind(deadQueName, deadExName, deadKey, null);

                    //发送消息(向正常队列发送)
                    //var prop=channel.CreateBasicProperties();
                    //prop.Expiration = "10000";//设置有效期 毫秒

                    int i = 0;
                    while (i < 15)
                    {
                        string message = $"测试死信 发送的第{i}条消息";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: normalExName, routingKey: normalKey, null, body);
                        Console.WriteLine($"测试死信,第{i}条消息发送成功");
                        i++;
                    }
                }
            }
        }
    }
}
