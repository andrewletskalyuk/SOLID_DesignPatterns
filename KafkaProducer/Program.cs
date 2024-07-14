using System;
using Confluent.Kafka;

namespace KafkaProducer;
class Program
{
    public static void Main(string[] args)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        using(var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            for (int i = 0; i < 10; i++)
            {
                var message = new Message<Null, string> { Value = $"Message{i}" };
                producer.Produce("my-topic", message, (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError)
                    {
                        Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine($"Produced message to {deliveryReport.TopicPartitionOffset}");
                    }
                });
            }

            // waiting ...
            producer.Flush(TimeSpan.FromSeconds(10));
        }
    }
}
