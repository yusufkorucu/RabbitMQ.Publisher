using RabbitMQ.Client;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();

//Crete Quee
channnel.QueueDeclare(queue: "korucu-test-quee", exclusive: false,durable:true);

IBasicProperties properties = channnel.CreateBasicProperties();
properties.Persistent= true;

//Sending Quee Message RabbitMq Quee sended message byte type

for (int i = 0; i < 25; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("korucu test message"+i);

    channnel.BasicPublish(exchange: "", routingKey: "korucu-test-quee", body: message,basicProperties:properties);
}


Console.Read();