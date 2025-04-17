using GTeams_backend.Data;
using GTeams_backend.GestaoPessoas.Dtos.EmailDtos;
using GTeams_backend.GestaoPessoas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.GestaoPessoas.Services;

public class EmailService(AppDbContext appDbContext)
{
    public async Task<bool> InserirAsync(InserirEmailDto emailDto)
    {
        Colaborador? colaborador = await appDbContext.Colaboradores
            .Include(c => c.Emails)
            .FirstOrDefaultAsync(c => c.Id == emailDto.ColaboradorId);

        if (colaborador == null)
            throw new ArgumentNullException(nameof(colaborador));
        
        
        bool emailJaExiste = await appDbContext.Emails
            .AnyAsync(e => e.Endereco == emailDto.Endereco && e.ColaboradorId == emailDto.ColaboradorId);
        
        if (emailJaExiste)
            throw new InvalidOperationException("Este e-mail já está cadastrado para o colaborador informado.");
        
        Email novoEmail = new Email
        {
            Descricao = emailDto.Descricao,
            Endereco = emailDto.Endereco,
            ColaboradorId = emailDto.ColaboradorId
        };
        
        await appDbContext.Emails.AddAsync(novoEmail);
        await appDbContext.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> DeletarAsync(int emailId)
    {
        Email? email = await appDbContext.Emails
            .FirstOrDefaultAsync(e => e.Id == emailId);

        if (email == null)
            throw new InvalidOperationException("E-mail não encontrado.");

        appDbContext.Emails.Remove(email);
        await appDbContext.SaveChangesAsync();

        return true;
    }
    
    public async Task<Email?> ObterPorIdAsync(int emailId)
    {
        Email? email = await appDbContext.Emails
            .FirstOrDefaultAsync(e => e.Id == emailId);
        return email;
    }
}