using System;
using LotoNET.Domain.Common;

namespace LotoNET.Domain.Entities;

public class Draw : BaseEntity
{
    public int DrawNumber { get; set; } //Ex: 2675
    public DateTime DrawDate { get; set; }

    //Dezenas sorteadas (não necessariamente em ordem)
    public List<int> NumbersDrawn { get; set; } = new();

    //Ordem real das bolas sorteadas, se disponível
    public List<int>? NumbersInOrder { get; set; }

    //Relacionamento
    public Guid LotteryId { get; set; }
    public Lottery? Lottery { get; set; }

}
