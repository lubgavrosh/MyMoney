using SQLite;

namespace MyMoney.Models;

public class Transaction
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Opis { get; set; } = "";
    public decimal Kwota { get; set; }
    public DateTime Data { get; set; }
    public bool CzyPrzychod { get; set; }
    public string Kategoria { get; set; } = "";

    [Ignore]
    public string Typ => CzyPrzychod ? "Przychód" : "Wydatek";
}