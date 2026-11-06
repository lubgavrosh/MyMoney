using MyMoney.ViewModels;

namespace MyMoney;

public partial class DetailsPage_1 : ContentPage
{
    public DetailsPage_1(SzczegolyViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
