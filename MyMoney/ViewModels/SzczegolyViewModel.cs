using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMoney.Models;

namespace MyMoney.ViewModels;

public partial class SzczegolyViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private Transaction? _transakcja;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Transakcja = query.TryGetValue("Transakcja", out var value)
            ? value as Transaction
            : null;
    }

    [RelayCommand]
    private async Task Wroc()
    {
        await Shell.Current.GoToAsync("..");
    }
}
