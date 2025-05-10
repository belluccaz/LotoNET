using System;

namespace LotoNET.Application.DTOs.UserPreferences;

public class UserPreferencesDto
{
    public string Theme { get; set; } = "light";
    public string SavedFilters { get; set; } = "{}";
    public string HighlightRules { get; set; } = "{}";
}
