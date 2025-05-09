using LotoNET.Domain.Entities;
using LotoNET.Application.DTOs.Resultados;

namespace LotoNET.Application.Mappers;

public static class ResultadoMapper
{
    public static ResultadoDto ToDto(Draw draw)
    {
        return new ResultadoDto
        {
            Concurso = draw.DrawNumber,
            DataSorteio = draw.DrawDate,
            Loteria = draw.Lottery?.Name ?? "Desconhecida",
            Dezenas = draw.NumbersDrawn,
            OrdemSorteio = draw.NumbersInOrder
        };
    }
}
