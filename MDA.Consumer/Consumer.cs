using System;
using System.Net.Security;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MDA.Consumer
{
    /// <summary>
    /// Consumer
    /// </summary>
    public class Consumer
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IModel _channel;
        private readonly string _queueName;

        public Consumer(string queueName)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "beaver-01.rmq.cloudamqp.com",
                VirtualHost = "xqchhxvp",
                UserName = "xqchhxvp", //Указать имя пользователя
                Password = "t57uK3jDXaJQgGDgm7OMDuoCAKDIXc9y", //Указать пароль
                Port = 5671,
                RequestedHeartbeat = TimeSpan.FromSeconds(10),
                Ssl = new SslOption
                {
                    Enabled = true,
                    AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch |
                                             SslPolicyErrors.RemoteCertificateChainErrors,
                    Version = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11
                }
            };

            _channel = _connectionFactory.CreateConnection().CreateModel();
            _queueName = queueName;
        }

        public void Receive(EventHandler<BasicDeliverEventArgs> receiveCallback)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += receiveCallback;

            _channel.BasicConsume(_queueName, true, consumer);
        }
    }
}
