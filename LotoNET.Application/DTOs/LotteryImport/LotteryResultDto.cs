using System;
using System.Text.Json.Serialization;

namespace LotoNET.Application.DTOs.LotteryImport;

public class LotteryResultDto
{
    public string Loteria { get; set; } = string.Empty;
    public int Concurso { get; set; }
    public string Data { get; set; } = string.Empty; // "07/05/2025"
    public string Local { get; set; } = string.Empty;

    public List<string> DezenasOrdemSorteio { get; set; } = new();
    public List<string> Dezenas { get; set; } = new();

    [JsonPropertyName("premiacoes")]
    public List<PremiacaoDto> Premiacoes { get; set; } = new();

    public bool Acumulou { get; set; }
    public int ProximoConcurso { get; set; }
    public string DataPRoximoConcurso { get; set; } = string.Empty;
}
