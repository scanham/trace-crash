using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace trace_crash
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var regularHttpClient = new HttpClient() { BaseAddress = new Uri("https://www.google.com/") };
            var customHandlerHttpClient = new HttpClient(new MyHandler()) { BaseAddress = new Uri("https://www.google.com/") };

            Console.WriteLine("Request https://www.google.com with regular HttpClient");
            var regularResult = await regularHttpClient.GetAsync("");
            Console.WriteLine(regularResult.ToString());

            Console.WriteLine("Request https://www.google.com with HttpClient with custom HttpClientHandler");
            var customResult = await customHandlerHttpClient.GetAsync("");
            Console.WriteLine(regularResult.ToString());
        }
    }

    public class MyHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Doing some work before sending message");
            var result = await base.SendAsync(request, cancellationToken);
            Console.WriteLine("Doing some work after sending message");
            return result;
        }
    }
}