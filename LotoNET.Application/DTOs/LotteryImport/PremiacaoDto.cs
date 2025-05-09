using System;

namespace LotoNET.Application.DTOs.LotteryImport;

public class PremiacaoDto
{
    public string Descricao { get; set; } = string.Empty;
    public int Faixa { get; set; }
    public int Ganhadores { get; set; }
    public decimal ValorPremio { get; set; }
}
