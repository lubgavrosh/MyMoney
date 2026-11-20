using MyMoney.ViewModels;

namespace MyMoney;

public partial class MainPage : ContentPage
{
    private readonly TransakcjeViewModel _viewModel;

    public MainPage(TransakcjeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()

    {
        base.OnAppearing();
        await _viewModel.WczytajCommand.ExecuteAsync(null);
    }
}
