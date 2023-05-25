using RabbitMQ.Client;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();

channnel.ExchangeDeclare(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic
    );


for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("korucu test message" + i);
    Console.WriteLine("topic gir");
    string topic = Console.ReadLine();
    
    channnel.BasicPublish(
        exchange: "topic-exchange-example", 
        routingKey: topic,
        body: message);
}


Console.Read();