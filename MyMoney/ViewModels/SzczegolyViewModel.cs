using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMoney.Models;
using MyMoney.Services;

namespace MyMoney.ViewModels;

public partial class SzczegolyViewModel : ObservableObject, IQueryAttributable
{
    private readonly TransakcjeBaza _baza;

    public SzczegolyViewModel(TransakcjeBaza baza)
    {
        _baza = baza;
    }

    [ObservableProperty]
    private Transaction? _transakcja;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Transakcja", out var value))
            Transakcja = value as Transaction;
    }

    [RelayCommand]
    private async Task Edytuj()
    {
        if (Transakcja is null)
            return;

        await Shell.Current.GoToAsync(nameof(FormularzPage),
            new ShellNavigationQueryParameters { ["Transakcja"] = Transakcja });
    }

    [RelayCommand]
    private async Task Usun()
    {
        var transakcja = Transakcja;
        if (transakcja is null)
            return;

        bool tak = await Shell.Current.DisplayAlert("Usunąć?", transakcja.Opis, "Tak", "Nie");
        if (!tak)
            return;

        await _baza.UsunAsync(transakcja);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Wroc()
    {
        await Shell.Current.GoToAsync("..");
    }
}
