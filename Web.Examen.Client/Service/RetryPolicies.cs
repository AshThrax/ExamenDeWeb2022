using Polly;
using Polly.Extensions.Http;

namespace Web.Examen.Client.Service
{
    //cette classe est dédiée uniquement au retry policy injecter dans les client Http dans le directories client
    //
    
    public class RetryPolicies
    {

        //retrypolicies
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg =>msg.StatusCode==System.Net.HttpStatusCode.NotFound)
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.BadRequest)
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Forbidden)
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(6,retry=> TimeSpan.FromSeconds(Math.Pow(2,retry)));
       }
        //circuit breaker 
        public  IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() 
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
