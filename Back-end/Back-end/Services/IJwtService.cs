using Back_end.DTOS.User;
using Back_end.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Back_end.Services
{
    public interface IJwtService
    {
        public AuthenticationResponseDTO CreateToken(ApplicationUser user);
    }
}
