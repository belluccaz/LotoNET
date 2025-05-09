using System;

namespace LotoNET.Application.DTOs.Resultados;

public class DezenaPorPosicaoDto
{
    public int Posicao { get; set; }
    public List<DezenaFrequenciaDto> Dezenas { get; set; } = new();
}
