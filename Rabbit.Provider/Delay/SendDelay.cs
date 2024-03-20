using Rabbit.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Provider.Delay
{
    /// <summary>
    /// 延时队列
    /// </summary>
    internal class SendDelay
    {
        public static void SendDelayedMsg()
        {
            using (var connection = RabbitMQHelper.GetConnect())
            using (var channel = connection.CreateModel())
            {
                // 定义一个延时类型的交换器
                channel.ExchangeDeclare("delayed_exchange", "x-delayed-message", durable: false, autoDelete: false, arguments: new Dictionary<string, object>
            {
                { "x-delayed-type", "direct" }
            });

                // 定义一个队列
                channel.QueueDeclare("delayed_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                // 将队列绑定到交换器上，并指定路由键
                channel.QueueBind("delayed_queue", "delayed_exchange", routingKey: "delayed_routing_key");

                // 发送延迟消息
                var body = Encoding.UTF8.GetBytes("这是我发送的延时消息");
                var prop = channel.CreateBasicProperties();
                prop.Headers = new Dictionary<string, object>() {
                    { "x-delay",15000}//延时15秒
                };
                channel.BasicPublish("delayed_exchange", "delayed_routing_key", basicProperties: prop, body: body);
            }
        }
    }
}
