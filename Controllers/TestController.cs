

using Microsoft.AspNetCore.Mvc;

namespace ProyectoInmobiliariaLab3.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{


private readonly DataContext context;

public TestController(DataContext dataContext)
        {
            this.context = dataContext;
        }



    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };



// GET api/<controller>/5
		[HttpGet()]
		public IActionResult Get()
		{
            
			return Ok("Prueba");
            //return Ok(id);    
      }  
}
 
      
/*
    private readonly ILogger<TestController> _logger;

   public TestController(ILogger<Test> logger)
    {
       // _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<Test> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Test
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

*/
