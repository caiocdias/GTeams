using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using GTeams_wpfapp.GestaoPessoas.Models;
using GTeams_wpfapp.GestaoPessoas.Models.ColaboradorDtos;

namespace GTeams_wpfapp.GestaoPessoas.Views.Colaborador
{
    public partial class CadastrarColaborador : INotifyPropertyChanged
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
        
        public InserirColaboradorDto InserirColaboradorDto { get; set; } = new InserirColaboradorDto();
        
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

        public void PrintarVariaveis(object sender, RoutedEventArgs routedEventArgs)
        {
            
            InserirColaboradorDto.Nome = TxtBoxNome.Text;
            InserirColaboradorDto.Cpf = TxtBoxCpf.Text;
            InserirColaboradorDto.Password = PwdBox.Password;
            InserirColaboradorDto.User = TxtBoxUser.Text;
            InserirColaboradorDto.Funcao = FuncaoSelecionada.Valor;
            
            Console.WriteLine(InserirColaboradorDto.Nome);
            Console.WriteLine(InserirColaboradorDto.Cpf);
            Console.WriteLine(InserirColaboradorDto.Password);
            Console.WriteLine(InserirColaboradorDto.User);
            Console.WriteLine(InserirColaboradorDto.Funcao);
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