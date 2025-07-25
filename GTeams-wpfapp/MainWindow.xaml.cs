﻿using System.Windows;
using System.Windows.Input;
using GTeams_wpfapp.GestaoPessoas.Views;
using GTeams_wpfapp.GestaoPessoas.Views.Colaborador;

namespace GTeams_wpfapp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
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

    private void LoadListarColaborador(object sender, RoutedEventArgs routedEventArgs)
    {
        MainContent.Content = null;
        MainContent.Content = new ListarColaborador();
    }
}