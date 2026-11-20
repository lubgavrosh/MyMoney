using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMoney.Models;
using MyMoney.Services;

namespace MyMoney.ViewModels;

public partial class FormularzViewModel : ObservableObject, IQueryAttributable
{
    private readonly TransakcjeBaza _baza;
    private int _id;

    public List<string> Kategorie { get; } = new()
    {
        "Jedzenie", "Pensja", "Transport", "Rachunki", "Rozrywka", "Inne"
    };

    [ObservableProperty]
    private string _tytul = "Nowa transakcja";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ZapiszCommand))]
    private string _opis = "";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ZapiszCommand))]
    private string _kwotaTekst = "";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ZapiszCommand))]
    private string? _kategoria;

    [ObservableProperty]
    private DateTime _data = DateTime.Today;

    [ObservableProperty]
    private bool _czyPrzychod;

    [ObservableProperty]
    private string _bladOpisu = "";

    [ObservableProperty]
    private string _bladKwoty = "";

    [ObservableProperty]
    private string _bladKategorii = "";

    public FormularzViewModel(TransakcjeBaza baza)
    {
        _baza = baza;
        Sprawdz();
    }

    partial void OnOpisChanged(string value) => Sprawdz();
    partial void OnKwotaTekstChanged(string value) => Sprawdz();
    partial void OnKategoriaChanged(string? value) => Sprawdz();

    // Akceptujemy przecinek i kropkę dziesiętną, bez separatorów tysięcy.
    private bool SprobujOdczytacKwote(out decimal kwota) => decimal.TryParse(
        (KwotaTekst ?? "").Trim().Replace(',', '.'),
        NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
        CultureInfo.InvariantCulture, out kwota);

    private void Sprawdz()
    {
        BladOpisu = string.IsNullOrWhiteSpace(Opis) ? "Podaj opis transakcji." : "";
        BladKwoty = !SprobujOdczytacKwote(out var kwota)
            ? "Wpisz poprawną kwotę, np. 125,50."
            : kwota <= 0
                ? "Kwota musi być większa od zera."
                : decimal.Round(kwota, 2) != kwota
                    ? "Kwota może mieć najwyżej dwa miejsca po przecinku."
                    : "";
        BladKategorii = Kategoria is null || !Kategorie.Contains(Kategoria)
            ? "Wybierz kategorię." : "";
    }

    private bool MoznaZapisac() =>
        BladOpisu.Length == 0 && BladKwoty.Length == 0 && BladKategorii.Length == 0;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("Transakcja", out var value) || value is not Transaction transakcja)
            return;

        _id = transakcja.Id;
        Opis = transakcja.Opis;
        KwotaTekst = transakcja.Kwota.ToString(CultureInfo.CurrentCulture);
        Data = transakcja.Data;
        CzyPrzychod = transakcja.CzyPrzychod;
        Kategoria = transakcja.Kategoria;
        Tytul = "Edycja";
        Sprawdz();
        ZapiszCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(MoznaZapisac))]
    private async Task Zapisz()
    {
        Sprawdz();
        if (!MoznaZapisac() || !SprobujOdczytacKwote(out var kwota))
            return;

        await _baza.ZapiszAsync(new Transaction
        {
            Id = _id,
            Opis = Opis.Trim(),
            Kwota = kwota,
            Data = Data,
            CzyPrzychod = CzyPrzychod,
            Kategoria = Kategoria!
        });
        await Shell.Current.GoToAsync(_id == 0 ? ".." : "../..");
    }

    [RelayCommand]
    private async Task Anuluj()
    {
        await Shell.Current.GoToAsync("..");
    }
}
