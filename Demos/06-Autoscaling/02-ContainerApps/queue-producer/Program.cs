using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;

namespace QueueApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var connectionString = configuration["msgqConStr"];
            var q = configuration["queueName"];
            QueueClient queue = new QueueClient(connectionString, q);
            
            for (int i = 0; i < 500; i++)
            {
                var value = "${'id': " + i + ", 'name': 'name" + i + "'}";
                await InsertMessageAsync(queue, value);
                System.Threading.Thread.Sleep(10);
                Console.WriteLine($"Sent: {value}");
            }
        }

        static async Task InsertMessageAsync(QueueClient q, string msg)
        {
            if (null != await q.CreateIfNotExistsAsync())
            {
                Console.WriteLine("The queue was created.");
            }

            await q.SendMessageAsync(msg);
        }
    }
}
