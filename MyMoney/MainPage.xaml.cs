using MyMoney.ViewModels;

namespace MyMoney;

public partial class MainPage : ContentPage
{
    public MainPage(TransakcjeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
