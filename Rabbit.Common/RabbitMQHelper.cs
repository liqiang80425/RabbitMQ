using RabbitMQ.Client;
using System.Collections.Generic;

namespace Rabbit.Common
{
    public class RabbitMQHelper
    {
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        public static IConnection GetConnect()
        {
            //连接工厂
            var fac = new ConnectionFactory()
            {
                HostName = "192.168.232.200",//IP
                Port=5671,//端口
                UserName="longma",
                Password= "longma",
                VirtualHost="/"
            };

            return fac.CreateConnection();
        }
    }
}