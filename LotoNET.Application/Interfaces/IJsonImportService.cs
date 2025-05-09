using System;

namespace LotoNET.Application.Interfaces;

public interface IJsonImportService
{
    Task ImportFromFileAsync(string filePath);
}
