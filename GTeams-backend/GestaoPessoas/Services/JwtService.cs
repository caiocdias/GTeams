using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GTeams_backend.GestaoPessoas.Models;
using Microsoft.IdentityModel.Tokens;

namespace GTeams_backend.GestaoPessoas.Services;

public class JwtService(IConfiguration config)
{
    private readonly string _secretKey = config["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT Secret is missing.");
    private readonly string _issuer = config["JwtSettings:Issuer"] ?? "GTeams";
    private readonly string _audience = config["JwtSettings:Audience"] ?? "GTeamsColaboradores";

    public string? GenerateToken(Colaborador colaborador)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, colaborador.Id.ToString()),
            new Claim("user", colaborador.User),
            new Claim("cpf", colaborador.Cpf),
            new Claim("funcao", colaborador.Funcao.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}