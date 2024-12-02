using Microsoft.AspNetCore.Mvc;
using Tea_Store.Models;
using Tea_Store.Services;

namespace Tea_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeaController : ControllerBase
    {
        private readonly ITeaService _teaService;

        public TeaController(ITeaService teaService)
        {
            _teaService = teaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tea>>> GetTeas(
            string? searchQuery = null,
            string? category = null,
            string? sortOrder = "price_asc",
            int pageNumber = 1,
            int pageSize = 6)
        {
            var teas = await _teaService.GetTeasAsync(searchQuery, category, sortOrder, pageNumber, pageSize);
            return Ok(teas);
        }
    }
}

