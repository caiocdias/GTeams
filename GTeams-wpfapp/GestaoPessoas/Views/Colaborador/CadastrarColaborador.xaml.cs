using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using GTeams_wpfapp.GestaoPessoas.Models;
using GTeams_wpfapp.GestaoPessoas.Models.ColaboradorDtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace GTeams_wpfapp.GestaoPessoas.Views.Colaborador
{
    public partial class CadastrarColaborador : INotifyPropertyChanged
    {
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
        public async void InserirAsync(object sender, RoutedEventArgs routedEventArgs)
        {
            InserirColaboradorDto inserirColaboradorDto = new InserirColaboradorDto
            {
                Nome = TxtBoxNome.Text,
                Cpf = TxtBoxCpf.Text,
                Password = PwdBox.Password,
                User = TxtBoxUser.Text,
                Funcao = FuncaoSelecionada.Valor,
                Ativo = CheckBoxAtivo.IsChecked ?? true
            };

            using HttpClient httpClient = new HttpClient();
            string url = "https://localhost:7075/api/Colaborador/Inserir";
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, inserirColaboradorDto);
            if (response.IsSuccessStatusCode)
                MessageBox.Show("Colaborador Inserido com Sucesso!");
            else
            {
                string erroContent = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Erro ao inserir colaborador: {erroContent}");
            }
        }

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
        
        //Controle de Enum
        public List<FuncaoComboItem> TipoFuncao { get; set; }
        private FuncaoComboItem _funcaoSelecionada;
        public class FuncaoComboItem
        {
            public string Nome { get; set; }
            public Funcao Valor { get; set; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
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