using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Toolbox.WebApi.CorrelationId;
using System.Net.Http;

namespace SampleApi1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ICorrelationContext _context;

        public ValuesController(ICorrelationContext context)
        {
            _context = context;
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

            //Three methods to add the correlation values to a request:

            //1. Set values directly on the request
            //request.Headers.Add(_context.IdHeaderKey, _context.CorrelationId.ToString());
            //request.Headers.Add(_context.SourceHeaderKey, _context.CorrelationSource);

            //2. Use the SetValuesOnRequest method
            //_context.SetValuesOnRequest(request);

            //3. Use an extension method on the request (preffered)
            request.SetCorrelationValues(_context);

            var client = new HttpClient();
            var result = await client.SendAsync(request);

            return await result.Content.ReadAsStringAsync();
        }

    }
}
