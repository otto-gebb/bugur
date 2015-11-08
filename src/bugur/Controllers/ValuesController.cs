namespace bugur.Controllers
{
	using System.Collections.Generic;
	using Microsoft.AspNet.Mvc;
	using Microsoft.Dnx.Runtime;

	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		private readonly IApplicationEnvironment _appEnvironment;

		public ValuesController(IApplicationEnvironment appEnvironment)
		{
			_appEnvironment = appEnvironment;
		}

		// GET: api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new[] { "value1", "value2", _appEnvironment.ApplicationBasePath };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
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