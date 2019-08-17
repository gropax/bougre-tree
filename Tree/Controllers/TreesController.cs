using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreesController : ControllerBase
    {
        // GET: api/Trees
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Trees/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Trees
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Trees/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Trees/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
