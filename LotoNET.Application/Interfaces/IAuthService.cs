using System;
using System.Threading.Tasks;
using LotoNET.Application.DTOs.Auth;

namespace LotoNET.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginWithGoogleAsync(GoogleLoginDto dto);
}
