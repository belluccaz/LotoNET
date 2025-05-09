using System;
using LotoNET.Domain.Common;

namespace LotoNET.Domain.Entities;

public class Lottery : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    // Relacionamento: Uma loteria tem vários sorteios

    public ICollection<Draw> Draws { get; set; } = new List<Draw>();
}
