using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTeams_backend.GestaoMetas.Enums;
using GTeams_backend.GestaoPessoas.Models;

namespace GTeams_backend.GestaoMetas.Models;

public class MetaColaboradorMedicao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("Colaborador")]
    public int ColaboradorId { get; set; }
    public Colaborador Colaborador { get; set; } = null!;
    
    [Required]
    [ForeignKey("IntervaloMedicao")]
    public int IntervaloMedicaoId { get; set; }
    public IntervaloMedicao IntervaloMedicao { get; set; } = null!;
    
    [Required]
    [ForeignKey("Equipe")]
    public int EquipeId { get; set; }
    public Equipe Equipe { get; set; } = null!;
    
    public decimal MetaNs { get; set; }
    public decimal MetaUs { get; set; }

    public List<DataPersonalizada> DatasPersonalizadasMedicao { get; set; } = new List<DataPersonalizada>();
    
    public bool GerarDatas()
    {
        if (DatasPersonalizadasMedicao.Any())
        {
            DatasPersonalizadasMedicao.Clear();
        }

        IEnumerable<DataPersonalizada> datas = Enumerable.Range(0, IntervaloMedicao.DataFinal.DayNumber - IntervaloMedicao.DataInicial.DayNumber + 1)
            .Select(offset => IntervaloMedicao.DataInicial.AddDays(offset))
            .Select(data => new DataPersonalizada
            {
                Data = data,
                TipoData = data.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday
                    ? TipoData.FimDeSemana
                    : TipoData.Util,
                MetaColaboradorMedicaoId = Id
            });

        DatasPersonalizadasMedicao.AddRange(datas);

        return true;
    }

}