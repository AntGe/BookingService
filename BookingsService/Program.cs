using BookingService.Data;
using BookingService.Model;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingsService
{
    class Program
    {
        private static readonly string kafkaEndpoint = "192.168.1.238:9092";
        private static readonly string kafkaTopic = "messages";
        static void Main(string[] args)
        {
            var bookingServiceContext = new BookingServiceContext();
            bookingServiceContext.GetService<IMigrator>().Migrate();
            var consumerConfig = new Dictionary<string, object>
                {
                    { "group.id", "booking" },
                    { "bootstrap.servers", kafkaEndpoint },
                    { "auto.offset.reset", "latest" }
                };

            using (var consumer = new Consumer<Null, string>(consumerConfig, null, new StringDeserializer(Encoding.UTF8)))
            {
                // Subscribe to the OnMessage event
                consumer.OnMessage += (obj, msg) =>
                {
                    if (msg.Value == "Success")
                    {
                        using (var context = new BookingServiceContext())
                        {
                            context.Events.Add(new Event() { Capacity = 4, Price = 111, Title = msg.ToString() });
                            context.SaveChanges();
                        }
                    }
                };

                // Subscribe to the Kafka topic
                consumer.Subscribe(new List<string>() { kafkaTopic });

               // Poll for messages
                
                while (true)
                {
                    consumer.Poll(5); 
                }
            }
        }
    }
}
