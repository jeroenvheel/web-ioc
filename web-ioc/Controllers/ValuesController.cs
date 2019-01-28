using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using web_ioc.Models;

namespace web_ioc.Controllers
{
    public class ValuesController : ApiController
    {
        private ISessionModel _session;

        public ValuesController(ISessionModel session)
        {
            _session = session;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
