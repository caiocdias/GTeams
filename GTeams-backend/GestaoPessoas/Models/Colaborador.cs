using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoPessoas.Enums;

namespace GTeams_backend.GestaoPessoas.Models;

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
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z0-9._-]{3,50}$", ErrorMessage = "O nome de usuário deve conter apenas letras, números, pontos, hífens ou underscores, e ter entre 3 e 50 caracteres.")]
    public string User { get; set; } = string.Empty;
    
    [Required]
    public byte[] PasswordHash { get; set; } = new byte[0];
    
    [StringLength(14)]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato 000.000.000-00.")]
    public string Cpf { get; set; } = "000.000.000-00";
    
    public bool Ativo { get; set; } = true;
    
    [Required]
    public Funcao Funcao { get; set; }
    
    public ICollection<Observacao> Observacoes { get; set; } = new List<Observacao>();
    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    public ICollection<Email> Emails { get; set; } = new List<Email>();
    
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
        if (PasswordHash.Length != SaltSize + KeySize)
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