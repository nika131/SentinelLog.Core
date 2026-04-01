using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using SecLog.Models;
using System.Collections.Generic;

namespace SecLog.Client
{
    class Program
    {
        static async Task Main(string[] args)
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

            await SendLog(e);

            Console.WriteLine("\n--- Current system logs---");
            await FetchAndPrintLogs();

            Console.WriteLine("\n Press any key to exit...");
            Console.ReadKey();
        }

        static async Task SendLog(SecurityEvent ev)
        {
            using (var client = new HttpClient())
            {
                var url = "http://localhost:60896/SecurityService.svc/LogEvent";

                client.DefaultRequestHeaders.Add("X-API-KEY", "Sentinel-1234");

                var settings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };

                var json = JsonConvert.SerializeObject(ev, settings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Sending {ev.EventType} to {url}...");

                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Service Response: " + result);
            }
        }


        static async Task FetchAndPrintLogs()
        {
            using (var client = new HttpClient())
            {
                var url = "http://localhost:60896/SecurityService.svc/GetLogs";

                client.DefaultRequestHeaders.Add("X-API-KEY", "Sentinel-1234");

                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var logs = JsonConvert.DeserializeObject<List<SecurityEvent>>(json);

                    foreach (var log in logs)
                    {
                        Console.WriteLine($"[{log.Timestamp}] {log.Severity} | {log.EventType} from {log.IpAddress}: {log.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"failed to fetch logs: {response.StatusCode}");
                }
            }
        }
    }
}