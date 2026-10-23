using System.Collections.ObjectModel;
using MyMoney.Models;

namespace MyMoney
{
    public partial class MainPage : ContentPage
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
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        private async void OnTransakcjaWybrana(object? sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is not Transaction transakcja)
            {
                return;
            }

            await Shell.Current.GoToAsync(nameof(DetailsPage_1),
                new ShellNavigationQueryParameters { ["Transakcja"] = transakcja });
        }
        private void OnDodajClicked(object? sender, EventArgs e)
        {
            Transakcje.Add(new Transaction
            {
                Opis = "Nowa transakcja",
                Kwota = 50m,
                Data = DateTime.Today,
                CzyPrzychod = false,
                Kategoria = "Inne"
            });
        }

    }
}
