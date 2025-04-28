using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using GTeams_wpfapp.GestaoPessoas.Models;
using GTeams_wpfapp.GestaoPessoas.Models.ColaboradorDtos;
using GTeams_wpfapp.GestaoPessoas.Models.EmailDtos;
using GTeams_wpfapp.GestaoPessoas.Models.MatriculaDtos;

namespace GTeams_wpfapp.GestaoPessoas.Views.Colaborador
{
    public partial class CadastrarColaborador : INotifyPropertyChanged
    {
        // ——— Coleções editáveis no DataGrid ———
        public ObservableCollection<InserirEmailDto> Emails { get; }
        public ObservableCollection<InserirMatriculaDto> Matriculas { get; }

        // ——— Seleção de função e campos simples ———
        public List<FuncaoComboItem> TipoFuncao { get; set; }
        private FuncaoComboItem _funcaoSelecionada;
        public FuncaoComboItem FuncaoSelecionada
        {
            get => _funcaoSelecionada;
            set { _funcaoSelecionada = value; OnPropertyChanged(); }
        }

        public CadastrarColaborador()
        {
            InitializeComponent();

            // Prepara lista de funções
            TipoFuncao = ((Funcao[])Enum.GetValues(typeof(Funcao)))
                .Select(f => new FuncaoComboItem
                {
                    Nome = GetEnumDisplayName(f),
                    Valor = f
                }).ToList();
            FuncaoSelecionada = TipoFuncao.First();

            // Inicializa as coleções com uma linha em branco
            Emails = new ObservableCollection<InserirEmailDto>
            {
                new InserirEmailDto()
            };
            Matriculas = new ObservableCollection<InserirMatriculaDto>
            {
                new InserirMatriculaDto()
            };

            DataContext = this;
        }

        public async void InserirAsync(object sender, RoutedEventArgs e)
        {
            // 1) Insere o colaborador
            var emailsValidos = Emails
                .Where(em => em != null
                            && !string.IsNullOrWhiteSpace(em.Descricao)
                            && !string.IsNullOrWhiteSpace(em.Endereco))
                .ToList();

            var matriculasValidas = Matriculas
                .Where(m => m != null
                            && !string.IsNullOrWhiteSpace(m.Codigo)
                            && !string.IsNullOrWhiteSpace(m.Descricao))
                .ToList();
            
            var colaboradorDto = new InserirColaboradorDto
            {
                Nome = TxtBoxNome.Text,
                Cpf = TxtBoxCpf.Text,
                Password = PwdBox.Password,
                User = TxtBoxUser.Text,
                Funcao = FuncaoSelecionada.Valor,
                Ativo = CheckBoxAtivo.IsChecked ?? true,
                Emails = emailsValidos,
                Matriculas = matriculasValidas,
            };

            using var http = new HttpClient { BaseAddress = new Uri("https://localhost:7075/") };
            var resp = await http.PostAsJsonAsync("api/Colaborador/Inserir", colaboradorDto);
            if (!resp.IsSuccessStatusCode)
            {
                var err = await resp.Content.ReadAsStringAsync();
                MessageBox.Show($"Erro ao inserir colaborador: {err}");
                return;
            }

            MessageBox.Show("Colaborador, e-mails e matrículas inseridos com sucesso!");
        }

        // ——— Helpers e INotify ———

        private string GetEnumDisplayName(Funcao value)
        {
            var attr = value.GetType()
                            .GetMember(value.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();
            return attr?.GetName() ?? value.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public class FuncaoComboItem
        {
            public string Nome { get; set; }
            public Funcao Valor { get; set; }
        }
    }
}
