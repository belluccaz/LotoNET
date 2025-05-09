using System;
using LotoNET.Application.DTOs.LotteryImport;

namespace LotoNET.Application.Interfaces;

public interface ILotteryApiService
{
    Task<List<LotteryResultDto>> FetchAllResultsAsync(string lotteryName);
}
