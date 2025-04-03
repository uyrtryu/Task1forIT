using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication8.ViewModels;

namespace AvaloniaApplication8.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}