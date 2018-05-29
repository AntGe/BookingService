using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsService
{
    public class Program
    {
        private static readonly string kafkaEndpoint = "192.168.1.238:9092";
        private static readonly string kafkaTopic = "messages";
        static void Main(string[] args)
        {  
            var consumerConfig = new Dictionary<string, object>
            {
                { "group.id", "payment" },
                { "bootstrap.servers", kafkaEndpoint },
                { "auto.offset.reset", "latest" }
            };

            // Create the consumer
            using (var consumer = new Consumer<Null, string>(consumerConfig, null, new StringDeserializer(Encoding.UTF8)))
            {
                // Subscribe to the OnMessage event
                consumer.OnMessage += (obj, msg) =>
                {
                    if (msg.Value == "pay")
                    { 
                        Produce();
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
        private static void Produce()
        {
            var producerConfig = new Dictionary<string, object> { { "bootstrap.servers", kafkaEndpoint } };

            // Create the producer
            using (var producer = new Producer<Null, string>(producerConfig, null, new StringSerializer(Encoding.UTF8)))
            { 
                    var result = producer.ProduceAsync(kafkaTopic, null, "Success").GetAwaiter().GetResult();
                    //Console.WriteLine($"Event {i} sent on Partition: {result.Partition} with Offset: {result.Offset}");
            }
        }
    } 
}  
