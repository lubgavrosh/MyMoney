using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMoney.Models;

namespace MyMoney.ViewModels;

public partial class TransakcjeViewModel : ObservableObject
{
        public ObservableCollection<Transaction> Transakcje { get; } = new()
        {
            new Transaction { Opis = "Zakupy w sklepie", Kwota = 125.50m, Data = new DateTime(2026, 10, 9),  CzyPrzychod = false, Kategoria = "Jedzenie" },
            new Transaction { Opis = "Pensja",           Kwota = 4200m,   Data = new DateTime(2026, 10, 1),  CzyPrzychod = true,  Kategoria = "Pensja" },
            new Transaction { Opis = "Bilet miesięczny", Kwota = 109m,    Data = new DateTime(2026, 10, 3),  CzyPrzychod = false, Kategoria = "Transport" },
            new Transaction { Opis = "Rachunek za prąd", Kwota = 230.40m, Data = new DateTime(2026, 10, 12), CzyPrzychod = false, Kategoria = "Rachunki" },
            new Transaction { Opis = "Kino",             Kwota = 45m,     Data = new DateTime(2026, 10, 15), CzyPrzychod = false, Kategoria = "Rozrywka" },
            new Transaction { Opis = "Zwrot podatku",    Kwota = 380m,    Data = new DateTime(2026, 10, 18), CzyPrzychod = true,  Kategoria = "Inne" },
        };

    [ObservableProperty]
    private int _liczbaTransakcji;

    public TransakcjeViewModel()
    {
        LiczbaTransakcji = Transakcje.Count;
    }

    [RelayCommand]
    private void Dodaj()
    {
        Transakcje.Add(new Transaction
        {
            Opis = "Nowa transakcja",
            Kwota = 50m,
            Data = DateTime.Today,
            CzyPrzychod = false,
            Kategoria = "Inne"
        });
        LiczbaTransakcji = Transakcje.Count;
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
