using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotoNET.Domain.Entities;

public class UserPreferences
{
    public Guid Id { get; set; }

    //FK e navegação
    public Guid UserId { get; set; }
    public User? User { get; set; }

    //Preferências personalizáveis
    public string Theme { get; set; } = "light"; // ou "dark"
    public string SavedFilters { get; set; } = "{}"; //JSON string (ex: concursos, datas, etc.)
    public string HighlightRules { get; set; } = "{}"; //JSON com regras visuais
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
