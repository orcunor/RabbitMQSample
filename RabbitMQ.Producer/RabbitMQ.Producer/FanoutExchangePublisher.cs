﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, arguments: ttl);

            int count = 0;

            while (true)
            {
                var message = new { Name = "Orçun", Message = $"Or! Count:{count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                //var properties = channel.CreateBasicProperties();
                //properties.Headers = new Dictionary<string, object> { {"accouunt" ,"new" } };

                channel.BasicPublish("demo-fanout-exchange", "account.new", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
