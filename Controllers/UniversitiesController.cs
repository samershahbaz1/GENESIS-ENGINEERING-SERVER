using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Model.DataModels;
using WebApplication1.Model.ViewModels;
using WebApplication1.Repo;

namespace WebApplication1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly ILogger<UniversitiesController> _logger;

        private readonly DataContext _context;
        private readonly HttpClient _httpClient;

        public UniversitiesController(ILogger<UniversitiesController> logger, DataContext context, HttpClient httpClient, UniversityRepository universityRepository)
        {
            _logger = logger;
            _context = context;
            this._httpClient = httpClient;
            _universityRepository = universityRepository;
        }

        private UniversityRepository _universityRepository { get; }

        [HttpGet("{country}")]
        public async Task<IActionResult> GetUniversities(string country)
        {
            var result = await _httpClient.GetAsync($"http://universities.hipolabs.com/search?country={country}");
            var universities = await result.Content.ReadAsStringAsync();
            await _universityRepository.SaveOrUpdate(country, JsonConvert.DeserializeObject<University[]>(universities));
            return Ok(JsonConvert.DeserializeObject<University[]>(universities));
        }
    }
}
