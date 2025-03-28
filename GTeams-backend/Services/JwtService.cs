using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GTeams_backend.Models;
using Microsoft.IdentityModel.Tokens;


namespace GTeams_backend.Services;

public class JwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtService(IConfiguration config)
    {
        _secretKey = config["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT Secret is missing.");
        _issuer = config["JwtSettings:Issuer"] ?? "GTeams";
        _audience = config["JwtSettings:Audience"] ?? "GTeamsColaboradores";
    }
    
    public string? GenerateToken(Colaborador colaborador)
    {
        var email = colaborador.Emails.FirstOrDefault()?.Endereco ?? "";
        if (email == string.Empty)
            return null;
            
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, colaborador.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
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