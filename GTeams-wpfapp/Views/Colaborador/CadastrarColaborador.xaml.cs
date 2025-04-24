using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using GTeams_wpfapp.Models;
using System.ComponentModel.DataAnnotations;

namespace GTeams_wpfapp.Views.Colaborador
{
    public partial class CadastrarColaborador : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public class FuncaoComboItem
        {
            public string Nome { get; set; }
            public Funcao Valor { get; set; }
        }

        public List<FuncaoComboItem> TipoFuncao { get; set; }

        private FuncaoComboItem _funcaoSelecionada;
        public FuncaoComboItem FuncaoSelecionada
        {
            get => _funcaoSelecionada;
            set
            {
                if (_funcaoSelecionada != value)
                {
                    _funcaoSelecionada = value;
                    OnPropertyChanged();
                }
            }
        }

        public CadastrarColaborador()
        {
            InitializeComponent();

            TipoFuncao = ((Funcao[])System.Enum.GetValues(typeof(Funcao))).Select(f => new FuncaoComboItem
            {
                Nome = GetEnumDisplayName(f),
                Valor = f
            }).ToList();

            FuncaoSelecionada = TipoFuncao.FirstOrDefault();
            DataContext = this;
        }

        private string GetEnumDisplayName(Funcao value)
        {
            var displayAttribute = value.GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.GetName() ?? value.ToString();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}