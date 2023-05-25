using RabbitMQ.Client;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();


channnel.ExchangeDeclare(exchange: "fanout-excahnge-example", type: ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message =Encoding.UTF8.GetBytes($"Merhaba {i}");

    //tüm kuyruklara bastıgı için routing key empty gecilir
    channnel.BasicPublish(exchange: "fanout-excahnge-example",
        routingKey: string.Empty,
        body: message);
}

Console.Read();