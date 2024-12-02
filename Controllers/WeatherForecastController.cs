using Microsoft.AspNetCore.Mvc;

namespace students_courses_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            // Step 1: Retrieve the Idempotency-Key from the Request Headers
            string idempotencyKey = string.Empty;
            if (Request.Headers.TryGetValue("Idempotency-Key", out var headerValues))
            {
                idempotencyKey = headerValues.FirstOrDefault(); // Get the first value if it exists
            }

            // Step 2: Add the Idempotency-Key to the Response Headers
            if (!string.IsNullOrEmpty(idempotencyKey))
            {
                Response.Headers.Add("Idempotency-Key", idempotencyKey);
            }

            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(forecasts);
        }
    }
}
