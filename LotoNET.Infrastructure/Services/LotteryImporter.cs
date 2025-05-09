using System;
using LotoNET.Application.Interfaces;
using LotoNET.Domain.Entities;
using LotoNET.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LotoNET.Infrastructure.Services;

public class LotteryImporter : ILotteryImporter
{
    private readonly LotoNetDbContext _context;
    private readonly ILotteryApiService _apiService;

    public LotteryImporter(LotoNetDbContext context, ILotteryApiService apiService)
    {
        _context = context;
        _apiService = apiService;
    }

    public async Task ImportFromApiAsync(string lotteryName)
    {
        Console.WriteLine($"🔄 Importando '{lotteryName}' direto da API...");

        var results = await _apiService.FetchAllResultsAsync(lotteryName);

        Lottery? lottery = await _context.Lotteries
            .FirstOrDefaultAsync(l => l.Name.ToLower() == lotteryName.ToLower());

        if (lottery == null)
        {
            lottery = new Lottery { Name = lotteryName };
            await _context.Lotteries.AddAsync(lottery);
            await _context.SaveChangesAsync();
        }

        foreach (var dto in results)
        {
            var exists = await _context.Draws
                .AsNoTracking()
                .AnyAsync(d => d.DrawNumber == dto.Concurso && d.LotteryId == lottery.Id);

            if (exists) continue;

            var draw = new Draw
            {
                LotteryId = lottery.Id,
                DrawNumber = dto.Concurso,
                DrawDate = DateTime.SpecifyKind(DateTime.Parse(dto.Data), DateTimeKind.Utc),
                NumbersDrawn = dto.Dezenas.Select(int.Parse).ToList(),
                NumbersInOrder = dto.DezenasOrdemSorteio?.Select(int.Parse).ToList()
            };

            await _context.Draws.AddAsync(draw);
        }

        await _context.SaveChangesAsync();

        Console.WriteLine($"✅ {results.Count} concursos de {lotteryName} importados.");
    }
}
