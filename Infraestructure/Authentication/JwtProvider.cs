using Domain.Usuarios;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Application.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Infraestructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        private readonly IRolesProvider _rolesProvider;
        public JwtProvider(IOptions<JwtOptions> options, IRolesProvider rolesProvider)
        {
            _options = options.Value;
            _rolesProvider = rolesProvider;
        }
        public async Task<string> Generar(Usuario usuario)
        {
            Claim[] claims = new Claim[]{
                new (JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new (JwtRegisteredClaimNames.Name,  usuario.Moderador ?? usuario.UserName!),
            };

            claims = [..claims, new Claim("roles", JsonSerializer.Serialize(await _rolesProvider.GetRoles(usuario)),JsonClaimValueTypes.JsonArray)];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddDays(5),
                signingCredentials
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}