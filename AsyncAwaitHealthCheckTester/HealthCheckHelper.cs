using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncAwaitHealthCheckTester
{
    public class HealthCheckHelper
    {
       
        public async Task<List<HealthCheck>>  HitEndpointAsync(List<string> urls)
        {
            int counter = 0;

            var healthChecks = new List<HealthCheck>();

            urls.ForEach(url =>
            {
                HttpClient _httpClient = new HttpClient();
                var response =  _httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    
                    healthChecks.Add(response.IsSuccessStatusCode
                        ? new HealthCheck()
                            {AppName = $"App-{counter++}", Status = HttpStatusCode.OK.ToString(), Url = url}
                        : new HealthCheck()
                        {
                            AppName = $"App-{counter++}", Status = response.StatusCode.ToString(), Url = url
                        });

                }
                else
                {
                   
                    healthChecks.Add(response.IsSuccessStatusCode
                        ? new HealthCheck()
                            { AppName = $"App-{counter++}", Status = HttpStatusCode.OK.ToString(), Url = url }
                        : new HealthCheck()
                        {
                            AppName = $"App-{counter++}",
                            Status = response.StatusCode.ToString(),
                            Url = url
                        });
                }
            });

            return healthChecks;
        }

        public async Task<HealthCheck> HitEndpointAsync(string url, int counter)
        {
           

            var healthChecks = new HealthCheck();

                HttpClient _httpClient = new HttpClient();
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {

                    healthChecks =response.IsSuccessStatusCode
                        ? new HealthCheck()
                            { AppName = $"App-{counter}", Status = HttpStatusCode.OK.ToString(), Url = url }
                        : new HealthCheck()
                        {
                            AppName = $"App-{counter}",
                            Status = response.StatusCode.ToString(),
                            Url = url
                        };

                }
                else
                {

                    healthChecks= response.IsSuccessStatusCode
                        ? new HealthCheck()
                            { AppName = $"App-{counter}", Status = HttpStatusCode.OK.ToString(), Url = url }
                        : new HealthCheck()
                        {
                            AppName = $"App-{counter}",
                            Status = response.StatusCode.ToString(),
                            Url = url
                        };
                }
          
            return healthChecks;
        }
    }
}
