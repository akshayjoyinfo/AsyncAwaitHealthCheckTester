using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AsyncAwaitHealthCheckTester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<HealthCheck>> Get()
        {
            try
            {

                FileHelper fh = new FileHelper();
                HealthCheckHelper helthCheck = new HealthCheckHelper();

                var urlsListTasks = await fh.GetUrlsFromFilesAsync();

               
                //var resultList =  await helthCheck.HitEndpointAsync(urlsListTasks);
                var result = await helthCheck.HitEndpointAsync(urlsListTasks);
                return result;
            }
            catch (Exception)
            {
                return new List<HealthCheck>();
            }

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<HealthCheck>> Get(int id)
        {
            try
            {

                FileHelper fh = new FileHelper();
                HealthCheckHelper helthCheck = new HealthCheckHelper();

                var urlsListTasks = await fh.GetUrlsFromFilesAsync();

                var taskList = new List<Task<HealthCheck>>();
                foreach (var myRequest in urlsListTasks)
                {
                    int counter = 0;
                    // by virtue of not awaiting each call, you've already acheived parallelism
                    taskList.Add(helthCheck.HitEndpointAsync(myRequest, ++counter));
                }



                //var resultList =  await helthCheck.HitEndpointAsync(urlsListTasks);
                var result = await Task.WhenAll(taskList.ToList());
                return result;
            }
            catch (Exception)
            {
                return new List<HealthCheck>();
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

   
}
