using Domain.Usuarios;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Application.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string Generar(Usuario usuario)
        {
            var claims = new Claim[]{
                new ("UserId", usuario.Id.Value.ToString()),
                new ("rango", usuario.Rango.ToString()),
                new (JwtRegisteredClaimNames.Name, usuario.Username.Value),

            };
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(1000),
                signingCredentials
            );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}