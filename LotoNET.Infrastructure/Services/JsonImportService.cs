using System;
using System.Text.Json;
using LotoNET.Application.DTOs.LotteryImport;
using LotoNET.Application.Interfaces;
using LotoNET.Domain.Entities;
using LotoNET.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LotoNET.Infrastructure.Services;

public class JsonImportService : IJsonImportService
{
    private readonly LotoNetDbContext _context;

    public JsonImportService(LotoNetDbContext context)
    {
        _context = context;
    }

    public async Task ImportFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Arquivo não encontrado: {filePath}");
            return;
        }

        var json = await File.ReadAllTextAsync(filePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var concursos = JsonSerializer.Deserialize<List<LotteryResultDto>>(json, options);

        if (concursos == null) return;

        foreach (var dto in concursos)
        {
            var existingDraw = await _context.Draws
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DrawNumber == dto.Concurso && d.Lottery!.Name.ToLower() == dto.Loteria.ToLower());

            if (existingDraw != null)
                continue;

            Lottery? lottery = await _context.Lotteries
                .FirstOrDefaultAsync(l => l.Name.ToLower() == dto.Loteria.ToLower());

            if (lottery == null)
            {
                lottery = new Lottery { Name = dto.Loteria };
                await _context.Lotteries.AddAsync(lottery);
                await _context.SaveChangesAsync();
            }

            var draw = new Draw
            {
                LotteryId = lottery.Id,
                DrawNumber = dto.Concurso,
                DrawDate = DateTime.Parse(dto.Data).ToUniversalTime(),
                NumbersDrawn = dto.Dezenas.Select(int.Parse).ToList(),
                NumbersInOrder = dto.DezenasOrdemSorteio?.Select(int.Parse).ToList()
            };

            await _context.Draws.AddAsync(draw);
        }

        await _context.SaveChangesAsync();

        Console.WriteLine($"Importação finalizada com sucesso: {concursos.Count} concursos analisados.");

    }


}
