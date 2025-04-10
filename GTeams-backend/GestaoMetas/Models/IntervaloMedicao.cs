using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTeams_backend.GestaoMetas.Enums;

namespace GTeams_backend.GestaoMetas.Models;

public class IntervaloMedicao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    public DateOnly DataInicial { get; set; }
    [Required]
    public DateOnly DataFinal { get; set; }
    
    public ICollection<DataPersonalizada> DatasPersonalizadasMedicao { get; set; } = new List<DataPersonalizada>();
    public ICollection<EquipeMetaMensal> EquipesMetasMensais { get; set; } = new List<EquipeMetaMensal>();
    
    public void GerarDatas()
    {
        if (DatasPersonalizadasMedicao.Any())
            DatasPersonalizadasMedicao.Clear();

        for (DateOnly data = DataInicial; data <= DataFinal; data = data.AddDays(1))
        {
            DatasPersonalizadasMedicao.Add(new DataPersonalizada
            {
                Data = data,
                TipoData = data.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday
                    ? TipoData.FimDeSemana
                    : TipoData.Util
            });
        }
    }
}