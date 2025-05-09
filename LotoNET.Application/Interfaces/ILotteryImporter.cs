using System;

namespace LotoNET.Application.Interfaces;

public interface ILotteryImporter
{
    Task ImportFromApiAsync(string lotteryName);
}
