using System.Text;
using Infraestructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Configuration {

    public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {

        private readonly JwtOptions _jwtOptions;

        public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public void Configure(JwtBearerOptions options)
        {
            
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
           options.TokenValidationParameters =new(){
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
            };
        }
    }
}
