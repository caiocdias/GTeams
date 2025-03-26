using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using GTeams_backend.Models.Enums;

namespace GTeams_backend.Models;

public class Colaborador
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000; 
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    public byte[] PasswordHash { get; set; } = new byte[0];
    
    [StringLength(14)]
    public string Cpf { get; set; } = string.Empty;
    
    [Required]
    [ForeignKey("Equipe")]
    public int EquipeId { get; set; }
    public Equipe Equipe { get; set; } = null!;
    
    [Required]
    public Funcao Funcao { get; set; }
    
    public ICollection<Observacao> Observacao { get; set; } = new List<Observacao>();
    public ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();
    public ICollection<DataPersonalizadaColaborador> DatasPersonalizadasColaborador { get; set; } = new List<DataPersonalizadaColaborador>();
    public void SetPassword(string password)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA512))
            {
                byte[] key = pbkdf2.GetBytes(KeySize);
                
                PasswordHash = new byte[SaltSize + KeySize];
                Array.Copy(salt, 0, PasswordHash, 0, SaltSize);
                Array.Copy(key, 0, PasswordHash, SaltSize, KeySize);
            }
        }
    }
    
    public bool ValidatePassword(string password)
    {
        if (PasswordHash == null || PasswordHash.Length != SaltSize + KeySize)
            return false;
        
        byte[] salt = new byte[SaltSize];
        Array.Copy(PasswordHash, 0, salt, 0, SaltSize);
        
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA512))
        {
            byte[] computedKey = pbkdf2.GetBytes(KeySize);
            
            for (int i = 0; i < KeySize; i++)
            {
                if (computedKey[i] != PasswordHash[SaltSize + i])
                    return false;
            }
            return true;
        }
    }
}