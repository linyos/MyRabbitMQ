using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQTEST.Model;

namespace RabbitMQTEST
{
    public class Program
    {
        private static MyUserInfo _userInfo = new MyUserInfo();

        public static void Main(string[] args)
        {
            string queueName = "MyQueue";
            string exchangeName = "MyExchange";
            string routingKeyName = "MyroutingKey";
            ConnectionFactory factory = new ConnectionFactory()
            {
                UserName = _userInfo.UserName,
                Password = _userInfo.PassWord,
                HostName = _userInfo.HostName
            };

            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                // 建立Queue
                channel.QueueDeclare(queue: queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                // 建立 exChange
                channel.ExchangeDeclare(exchange: exchangeName,
                                        durable: false,
                                        autoDelete: false,
                                        arguments: null,
                                        type: ExchangeType.Direct);

                // 綁定 Queue跟 exchange
                channel.QueueBind(queueName, exchangeName, routingKeyName);

                Console.WriteLine("\n RabbitMQ連接成功");

                string input = string.Empty;
                do
                {
                    input = Console.ReadLine();
                    var sendBytes = Encoding.UTF8.GetBytes(input);
                    // 發布訊息到RabbitMQ 到Service
                    channel.BasicPublish(exchangeName, routingKeyName, null, sendBytes);
                }
                while (Console.ReadKey().Key != ConsoleKey.Escape);
            }
        }
    }
}