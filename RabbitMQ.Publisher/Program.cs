using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active Channeş Open

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();

// direct exchange

channnel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

while (true)
{

    Console.Write("Mesaj:");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);
    channnel.BasicPublish(
        exchange: "direct-exchange-example",
        routingKey: "direct-quee-example",
        body: byteMessage);
}
Console.Read();