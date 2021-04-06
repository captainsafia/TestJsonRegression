using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Diagnostics;

namespace TestApp
{
    public class Program
    {
        public static string Data = @"[
            {
                ""Date"": ""2018-05-06"",
                ""TemperatureC"": 1,
                ""Summary"": ""Freezing""
            },
            {
                ""Date"": ""2018-05-07"",
                ""TemperatureC"": 14,
                ""Summary"": ""Bracing""
            },
            {
                ""Date"": ""2018-05-08"",
                ""TemperatureC"": -13,
                ""Summary"": ""Freezing""
            },
            {
                ""Date"": ""2018-05-09"",
                ""TemperatureC"": -16,
                ""Summary"": ""Balmy""
            },
            {
                ""Date"": ""2018-05-10"",
                ""TemperatureC"": -2,
                ""Summary"": ""Chilly""
            },
                {
                ""Date"": ""2018-05-06"",
                ""TemperatureC"": 1,
                ""Summary"": ""Freezing""
            },
            {
                ""Date"": ""2018-05-07"",
                ""TemperatureC"": 14,
                ""Summary"": ""Bracing""
            },
            {
                ""Date"": ""2018-05-08"",
                ""TemperatureC"": -13,
                ""Summary"": ""Freezing""
            },
            {
                ""Date"": ""2018-05-09"",
                ""TemperatureC"": -16,
                ""Summary"": ""Balmy""
            },
            {
                ""Date"": ""2018-05-10"",
                ""TemperatureC"": -2,
                ""Summary"": ""Chilly""
            }
        ]";
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            WeatherForecast[] weatherForecast = null;
            
            for (var i = 0; i <= 10; i++)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                weatherForecast = JsonSerializer.Deserialize<WeatherForecast[]>(Data);
                stopWatch.Stop();

                System.Console.WriteLine($"Deserialize {i}: {stopWatch.Elapsed.TotalMilliseconds} milliseconds");
            }


            for (var i = 0; i <= 10; i++)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                byte[] jsonUtf8Bytes;
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(weatherForecast, options);
                stopWatch.Stop();

                System.Console.WriteLine($"Serialize {i}: {stopWatch.Elapsed.TotalMilliseconds} milliseconds");
            }

            await builder.Build().RunAsync();
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}
