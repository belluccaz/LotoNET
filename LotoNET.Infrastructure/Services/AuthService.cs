using LotoNET.Application.Interfaces;
using LotoNET.Application.DTOs.Auth;
using LotoNET.Domain.Entities;
using LotoNET.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace LotoNET.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly LotoNetDbContext _context;
    private readonly IJwtTokenService _jwtService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public AuthService(
        LotoNetDbContext context,
        IJwtTokenService jwtService,
        IHttpClientFactory httpClientFactory,
        IConfiguration config)
    {
        _context = context;
        _jwtService = jwtService;
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    public async Task<AuthResponseDto?> LoginWithGoogleAsync(GoogleLoginDto dto)
    {
        var http = _httpClientFactory.CreateClient();
        var response = await http.GetFromJsonAsync<GoogleUserInfoDto>($"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={dto.IdToken}");

        if (response is null || string.IsNullOrEmpty(response.Email))
            return null;

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == response.Email);

        if (user is null)
        {
            user = new User
            {
                GoogleId = response.Sub,
                Email = response.Email,
                Name = response.Name,
                PictureUrl = response.Picture
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        var token = _jwtService.GenerateToken(user.Id, user.Email);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Name = user.Name,
            PictureUrl = user.PictureUrl
        };
    }
}
