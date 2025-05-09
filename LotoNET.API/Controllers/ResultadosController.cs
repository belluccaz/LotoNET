using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LotoNET.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using LotoNET.Application.DTOs.Resultados;
using LotoNET.Application.Mappers;

namespace LotoNET.API.Controllers
{
    [Route("api/resultados")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        private readonly LotoNetDbContext _context;

        public ResultadosController(LotoNetDbContext context)
        {
            _context = context;
        }

        // GET: /api/resultados
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var resultados = await _context.Draws
                .Include(d => d.Lottery)
                .AsNoTracking()
                .Select(d => ResultadoMapper.ToDto(d))
                .ToListAsync();

            return Ok(resultados);
        }

        [HttpGet("{loteria}")]
        public async Task<IActionResult> ObterPorLoteria(string loteria)
        {
            var resultados = await _context.Draws
                .Include(d => d.Lottery)
                .Where(d => d.Lottery != null && d.Lottery.Name.ToLower() == loteria.ToLower())
                .OrderByDescending(d => d.DrawNumber)
                .Select(d => ResultadoMapper.ToDto(d))
                .ToListAsync();

            if (!resultados.Any())
                return NotFound("Nenhum resultado encontrado para a loteria informada.");

            return Ok(resultados);
        }

        [HttpGet("ultimos/{loteria}")]
        public async Task<IActionResult> ObterUltimosResultados(string loteria)
        {
            var resultados = await _context.Draws
                .Include(d => d.Lottery)
                .Where(d => d.Lottery != null && d.Lottery.Name.ToLower() == loteria.ToLower())
                .OrderByDescending(d => d.DrawNumber)
                .Take(10)
                .Select(d => ResultadoMapper.ToDto(d))
                .ToListAsync();

            return Ok(resultados);
        }

        [HttpGet("mais-sorteados/{loteria}")]
        public async Task<IActionResult> ObterDezenasMaisSorteadas(string loteria)
        {
            var dezenas = await _context.Draws
                .Include(d => d.Lottery)
                .Where(d => d.Lottery != null && d.Lottery.Name.ToLower() == loteria.ToLower())
                .SelectMany(d => d.NumbersDrawn)
                .GroupBy(num => num)
                .Select(g => new DezenaFrequenciaDto
                {
                    Dezena = g.Key.ToString(),
                    Quantidade = g.Count()
                })
                .OrderByDescending(x => x.Quantidade)
                .ToListAsync();

            if (!dezenas.Any())
                return NotFound("Nenhuma dezena encontrada para a loteria informada.");

            return Ok(dezenas);
        }
        [HttpGet("{loteria}/{concurso}")]
        public async Task<IActionResult> ObterResultadoPorConcurso(string loteria, int concurso)
        {
            var draw = await _context.Draws
                .Include(d => d.Lottery)
                .Where(d => d.Lottery != null &&
                            d.Lottery.Name.ToLower() == loteria.ToLower() &&
                            d.DrawNumber == concurso)
                .FirstOrDefaultAsync();

            if (draw == null)
                return NotFound("Concurso não encontrado para a loteria informada.");

            var resultadoDto = ResultadoMapper.ToDto(draw);
            return Ok(resultadoDto);
        }
        [HttpGet("frequencia-por-posicao/{loteria}")]
        public async Task<IActionResult> ObterFrequenciaPorPosicao(string loteria)
        {
            var draws = await _context.Draws
                .Include(d => d.Lottery)
                .Where(d => d.Lottery != null && d.Lottery.Name.ToLower() == loteria.ToLower())
                .ToListAsync();

            if (!draws.Any())
                return NotFound("Nenhum resultado encontrado para a loteria informada.");

            int maxPosicoes = draws.Max(d => d.NumbersDrawn.Count);
            var resultado = new List<DezenaPorPosicaoDto>();

            for (int i = 0; i < maxPosicoes; i++)
            {
                var dezenasNaPosicao = draws
                    .Where(d => d.NumbersDrawn.Count > i)
                    .Select(d => d.NumbersDrawn[i])
                    .GroupBy(n => n)
                    .Select(g => new DezenaFrequenciaDto
                    {
                        Dezena = g.Key.ToString(),
                        Quantidade = g.Count()
                    })
                    .OrderByDescending(x => x.Quantidade)
                    .ToList();

                resultado.Add(new DezenaPorPosicaoDto
                {
                    Posicao = i + 1,
                    Dezenas = dezenasNaPosicao
                });
            }

            return Ok(resultado);
        }

    }
}
