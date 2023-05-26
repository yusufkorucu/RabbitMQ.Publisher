using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();

#region P2P (Point to Point)

//string queueName = "example-p2p-queue";

//channnel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//byte[] message = Encoding.UTF8.GetBytes("Merhaba Korucu");
//channnel.BasicPublish(exchange: string.Empty,
//    routingKey: queueName,
//    body: message);

#endregion

#region Publish/Subscribe (pub/sub)

//string exchangeName = "example-pub-sub-exchange";
//channnel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);

//for (int i = 0; i < 100; i++)
//{

//    byte[] message = Encoding.UTF8.GetBytes($"Merhaba KORUCU {i}");

//    channnel.BasicPublish(
//        exchange: exchangeName,
//        routingKey: string.Empty,
//        body: message
//        );
//}

#endregion

#region Work Quee(İş kuyruğu)

//string queueName = "example-work-queue";

//channnel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);



//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(185);
//    byte[] message = Encoding.UTF8.GetBytes($"Merhaba KORUCU {i}");

//    channnel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: queueName,
//        body: message
//        );
//}

#endregion

#region Request Response 
string requestQueueName = "example-request-response-queue";

channnel.QueueDeclare(queue: requestQueueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

string replyQueueName = channnel.QueueDeclare().QueueName;

string correlationId = Guid.NewGuid().ToString();

#region Request Mesajı oluştur ve Gönder

IBasicProperties properties = channnel.CreateBasicProperties();
properties.CorrelationId = correlationId;

properties.ReplyTo = replyQueueName;

for (int i = 0; i < 10; i++)
{

    byte[] message = Encoding.UTF8.GetBytes($"KORUCU {i}");

    channnel.BasicPublish(
        exchange: string.Empty,
        routingKey: requestQueueName,
        body: message,
        basicProperties: properties);
}

#endregion

#region Response Mesajı Dinleme

EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);
channnel.BasicConsume(queue: replyQueueName,
    consumer: consumer, autoAck: true);

consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{

    if (e.BasicProperties.CorrelationId == correlationId)
    {
        Console.WriteLine($"Response {Encoding.UTF8.GetString(e.Body.Span)}");
    }
}

#endregion


#endregion


Console.Read();