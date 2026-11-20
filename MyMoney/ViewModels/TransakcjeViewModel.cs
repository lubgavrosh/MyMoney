using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMoney.Models;
using MyMoney.Services;

namespace MyMoney.ViewModels;

public partial class TransakcjeViewModel : ObservableObject
{
    public ObservableCollection<Transaction> Transakcje { get; } = new();
    private readonly TransakcjeBaza _baza;

    [ObservableProperty]
    private int _liczbaTransakcji;

    public TransakcjeViewModel(TransakcjeBaza baza)
    {
        _baza = baza;
    }

    [RelayCommand]
    private async Task Wczytaj()
    {
        var transakcje = await _baza.PobierzWszystkieAsync();
        Transakcje.Clear();
        foreach (var transakcja in transakcje)
            Transakcje.Add(transakcja);
        LiczbaTransakcji = Transakcje.Count;
    }

    [RelayCommand]
    private async Task Dodaj()
    {
        await Shell.Current.GoToAsync(nameof(FormularzPage));
    }

    [RelayCommand]
    private async Task PokazSzczegoly(Transaction? transakcja)
    {
        if (transakcja is null)
            return;

        await Shell.Current.GoToAsync(nameof(DetailsPage_1),
            new ShellNavigationQueryParameters { ["Transakcja"] = transakcja });
    }
}
