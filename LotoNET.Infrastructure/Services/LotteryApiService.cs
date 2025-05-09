using System;
using System.Text.Json;
using LotoNET.Application.DTOs.LotteryImport;
using LotoNET.Application.Interfaces;

namespace LotoNET.Infrastructure.Services;

public class LotteryApiService : ILotteryApiService
{
    private readonly HttpClient _httpClient;

    public LotteryApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LotteryResultDto>> FetchAllResultsAsync(string lotteryName)
    {
        var url = $"https://loteriascaixa-api.herokuapp.com/api/{lotteryName}";

        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var concursos = JsonSerializer.Deserialize<List<LotteryResultDto>>(content, options)
                        ?? new List<LotteryResultDto>();

        return concursos;
    }
}
