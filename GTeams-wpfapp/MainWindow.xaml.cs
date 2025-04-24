using System.Windows;
using System.Windows.Input;
using GTeams_wpfapp.Views;
using GTeams_wpfapp.Views.Colaborador;

namespace GTeams_wpfapp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainContent.Content = null;
        MainContent.Content = new Home();
    }
    
    private void LoadHome(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        MainContent.Content = null;
        MainContent.Content = new Home();
    }

    private void LoadCadastrarColaborador(object sender, RoutedEventArgs routedEventArgs)
    {
        MainContent.Content = null;
        MainContent.Content = new CadastrarColaborador();
    }
}