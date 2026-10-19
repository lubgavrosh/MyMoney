namespace MyMoney;

[QueryProperty(nameof(TransakcjaId), "id")]
public partial class DetailsPage_1 : ContentPage
{
    string transakcjaId = "";

    public string TransakcjaId
    {
        get => transakcjaId;
        set
        {
            transakcjaId = value;
            NumerLabel.Text = $"Transakcja nr {value}";
            
        }
    }

    public DetailsPage_1()
    {
        InitializeComponent();
    }

    private async void OnWrocClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}