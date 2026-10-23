using MyMoney.Models;
namespace MyMoney;

[QueryProperty(nameof(Transakcja), "Transakcja")]
public partial class DetailsPage_1 : ContentPage
{
    public Transaction Transakcja { set => BindingContext = value; }


 

    public DetailsPage_1()
    {
        InitializeComponent();
    }

    private async void OnWrocClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
