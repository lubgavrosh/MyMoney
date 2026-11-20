using MyMoney.Models;
using SQLite;

namespace MyMoney.Services;

public class TransakcjeBaza
{
    private SQLiteAsyncConnection? _db;
    private readonly SemaphoreSlim _initLock = new(1, 1);

    private async Task<SQLiteAsyncConnection> Init()
    {
        await _initLock.WaitAsync();
        try
        {
            if (_db is not null)
                return _db;

            var sciezka = Path.Combine(FileSystem.AppDataDirectory, "transakcje.db3");
            var db = new SQLiteAsyncConnection(sciezka);
            await db.CreateTableAsync<Transaction>();

            if (await db.Table<Transaction>().CountAsync() == 0)
            {
                await db.InsertAllAsync(new[]
                {
                    new Transaction { Opis = "Zakupy w sklepie", Kwota = 125.50m, Data = new DateTime(2026, 10, 9), CzyPrzychod = false, Kategoria = "Jedzenie" },
                    new Transaction { Opis = "Pensja", Kwota = 4200m, Data = new DateTime(2026, 10, 1), CzyPrzychod = true, Kategoria = "Pensja" },
                    new Transaction { Opis = "Bilet miesięczny", Kwota = 109m, Data = new DateTime(2026, 10, 3), CzyPrzychod = false, Kategoria = "Transport" },
                    new Transaction { Opis = "Rachunek za prąd", Kwota = 230.40m, Data = new DateTime(2026, 10, 12), CzyPrzychod = false, Kategoria = "Rachunki" },
                    new Transaction { Opis = "Kino", Kwota = 45m, Data = new DateTime(2026, 10, 15), CzyPrzychod = false, Kategoria = "Rozrywka" },
                    new Transaction { Opis = "Zwrot podatku", Kwota = 380m, Data = new DateTime(2026, 10, 18), CzyPrzychod = true, Kategoria = "Inne" }
                });
            }

            _db = db;
            return db;
        }
        finally
        {
            _initLock.Release();
        }
    }

    public async Task<List<Transaction>> PobierzWszystkieAsync()
    {
        var db = await Init();
        return await db.Table<Transaction>().OrderBy(t => t.Id).ToListAsync();
    }

    public async Task ZapiszAsync(Transaction transakcja)
    {
        var db = await Init();
        if (transakcja.Id == 0)
            await db.InsertAsync(transakcja);
        else
            await db.UpdateAsync(transakcja);
    }

    public async Task UsunAsync(Transaction transakcja)
    {
        var db = await Init();
        await db.DeleteAsync(transakcja);
    }
}