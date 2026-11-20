using MyMoney.ViewModels;

namespace MyMoney;

public partial class FormularzPage : ContentPage
{
    public FormularzPage(FormularzViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
