using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using WebApplication1.Model.DataModels;
using WebApplication1.Model.ViewModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;

        private readonly DataContext _context;
        public CountriesController(ILogger<CountriesController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> PostCountry([FromBody] Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return Ok(country);
        }
    }
}
