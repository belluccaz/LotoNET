using LotoNET.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LotoNET.API.Controllers
{
    [Route("api/loterias")]
    [ApiController]
    public class LoteriasController : ControllerBase
    {
        private readonly LotoNetDbContext _context;

        public LoteriasController(LotoNetDbContext context)
        {
            _context = context;
        }

        // Get: /api/loterias
        [HttpGet]
        public async Task<IActionResult> GetLotteries()
        {
            var lotteries = await _context.Lotteries
                .AsNoTracking()
                .ToListAsync();

            return Ok(lotteries);
        }
    }
}
