using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Toolbox.WebApi.CorrelationId;
using System.Net.Http;
using Microsoft.AspNet.Http;
using SampleApi1.ServiceAgents;

namespace SampleApi1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ICorrelationContext _context;
        private SampleApi2Agent _sampleAgent;

        public ValuesController(ICorrelationContext context, SampleApi2Agent sampleAgent)
        {
            _context = context;
            _sampleAgent = sampleAgent;
        }

        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return $"Result form SampleApi1: CorrelationId = {_context.CorrelationId.ToString()}, CorrelationSource = {_context.CorrelationSource}";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            //call antother api
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:5001/api/Values"),
                Method = HttpMethod.Get,
            };

            //Set headers on the request
            request.SetCorrelationValues(_context);

            var client = new HttpClient();
            var result = await client.SendAsync(request);

            return await result.Content.ReadAsStringAsync();
        }

        // GET api/values/5
        [HttpGet("UseServiceAgent")]
        public async Task<string> UseServiceAgent()
        {
            var result =  await _sampleAgent.GetMessage();

            return result.Value;
        }

    }
}
