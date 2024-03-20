// See https://aka.ms/new-console-template for more information
using Rabbit.Provider.Dead;
using Rabbit.Provider.Delay;
using Rabbit.Provider.Helloword;
using Rabbit.Provider.PubSub;
using Rabbit.Provider.Routing;
using Rabbit.Provider.Topics;
using Rabbit.Provider.WorkQueue;

//Helloword模式
//SendHelloword.SendHellowordMsg();

//工作队列模式
//SendWorkQueue.SendWorkQueueMsg();

//发布 订阅
SendPubSubQueue.SendPubSubMsg();

//路由模式
//SendRoutingQueue.SendRoutingMsg();

//主题模式/关键字模式
//SendTopicsQueue.SendTopicsMsg();

//死信
//SendDead.SendDeadMsg();

//延时
//SendDelay.SendDelayedMsg();
Console.WriteLine("所有消息发送完毕!");
Console.ReadKey();
