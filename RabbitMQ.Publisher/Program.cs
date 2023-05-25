using RabbitMQ.Client;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();


channnel.ExchangeDeclare(
    exchange: "header-exchange-example",
    type: ExchangeType.Headers
    );



//Sending Quee Message RabbitMq Quee sended message byte type

for (int i = 0; i < 25; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("korucu test message" + i);

    Console.Write("Header value gir: ");
    string value = Console.ReadLine();

    var basicProperties = channnel.CreateBasicProperties();
    basicProperties.Headers = new Dictionary<string, object>()
    {
        ["no"] = value
    };
    channnel.BasicPublish(exchange: "header-exchange-example", routingKey: string.Empty, body: message,
        basicProperties: basicProperties);

}


Console.Read();