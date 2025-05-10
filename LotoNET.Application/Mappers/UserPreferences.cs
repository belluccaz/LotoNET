using System;
using LotoNET.Application.DTOs.UserPreferences;
using LotoNET.Domain.Entities;

namespace LotoNET.Application.Mappers;

public class UserPreferencesMapper
{
    public static UserPreferencesDto ToDto(UserPreferences prefs) => new()
    {
        Theme = prefs.Theme,
        SavedFilters = prefs.SavedFilters,
        HighlightRules = prefs.HighlightRules
    };

    public static void UpdateFromDto(UserPreferences prefs, UserPreferencesDto dto)
    {
        prefs.Theme = dto.Theme;
        prefs.SavedFilters = dto.SavedFilters;
        prefs.HighlightRules = dto.HighlightRules;
        prefs.LastUpdated = DateTime.UtcNow;
    }
}
