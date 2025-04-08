using System.ComponentModel.DataAnnotations;
using GTeams_backend.GestaoPessoas.Models;

namespace GTeams_backend.GestaoMetas.Models;

public class ColaboradorEquipeMetaMensal
{
        [Key]
        public int Id { get; set; }

        [Required]
        public int ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; } = null!;

        [Required]
        public int EquipeMetaMensalId { get; set; }
        public EquipeMetaMensal EquipeMetaMensal { get; set; } = null!;

        public bool Ativo { get; set; } = true;
}