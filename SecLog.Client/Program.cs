using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json; 
using SecLog.Models;

namespace SecLog.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SentinelLog Security Client ===");

            var e = new SecurityEvent
            {
                SourceSystem = "Gateway-Alpha",
                EventType = "Unrecognized IP Attempt",
                Severity = SeverityLevel.Critical,
                Message = "Multiple failed handshake attempts from external range.",
                IpAddress = "192.168.1.55",
                Timestamp = DateTime.Now
            };

            SendLog(e);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static async void SendLog(SecurityEvent ev)
        {
            using (var client = new HttpClient())
            {
                var url = "http://localhost:60896/SecurityService.svc/LogEvent";

                var json = JsonConvert.SerializeObject(ev);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Sending {ev.EventType} to {url}...");

                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Service Response: " + result);
            }
        }
    }
}