// See https://aka.ms/new-console-template for more information
using Rabbit.Consumer.Dead;
using Rabbit.Consumer.Delay;
using Rabbit.Consumer.Helloword;
using Rabbit.Consumer.PubSub;
using Rabbit.Consumer.Routing;
using Rabbit.Consumer.Topics;
using Rabbit.Consumer.WorkQueue;

//Helloword模式
//ReceiveHelloword.ReceiveHellowordMsg();

//工作队列模式
//ReceiveWorkQueue.ReceiveWorkMsg();

//发布 订阅
//ReceivePubSubQueue.ReceivePubSubMsg();

//路由模式/完全匹配模式
ReceiveRoutingQueue.ReceiveRoutingMsg();

//关键字模式
//ReceiveTopicsQueue.ReceiveTopicsMsg();

//死信
//ReceiveDead.ReceiveDeadMsg();

//消费死信队列里的
//ReceiveDeadQue.ReceiveDeadQueMsg();

//延时
//ReceiveDelay.ReceiveDelayMsg();

Console.WriteLine("消费端已经启动!");


Console.ReadKey();