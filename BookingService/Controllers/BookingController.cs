using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks; 
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Produces("application/json")]
    [Route("api/Booking")]
    public class BookingController : Controller
    { 

        public BookingController()
        { 
        }
        // GET api/values
        [HttpGet]
        public HttpResponseMessage Get()
        {  
            string kafkaEndpoint = "192.168.1.238:9092";

            // The Kafka topic we'll be using
            string kafkaTopic = "messages";

            // Create the producer configuration
            var producerConfig = new Dictionary<string, object> { { "bootstrap.servers", kafkaEndpoint } };

            // Create the producer
            using (var producer = new Producer<Null, string>(producerConfig, null, new StringSerializer(Encoding.UTF8)))
            {
                
                    var result = producer.ProduceAsync(kafkaTopic, null, "pay").GetAwaiter().GetResult();
                    //Console.WriteLine($"Event {i} sent on Partition: {result.Partition} with Offset: {result.Offset}"); 
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
    }
}