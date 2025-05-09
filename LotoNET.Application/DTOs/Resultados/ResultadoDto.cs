using System;

namespace LotoNET.Application.DTOs.Resultados;

public class ResultadoDto
{
    public string Loteria { get; set; } = string.Empty;
    public int Concurso { get; set; }
    public DateTime DataSorteio { get; set; }
    public List<int> Dezenas { get; set; } = new();
    public List<int>? OrdemSorteio { get; set; }
}
