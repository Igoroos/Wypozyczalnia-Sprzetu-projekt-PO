namespace WypozyczalniaSprzetuGorskiego.Models
{
    public class Pracownik : Uzytkownik
    {
        public string Stanowisko { get; set; } = string.Empty;

        public Pracownik()
        {
        }

        public Pracownik(int id, string imie, string nazwisko, string telefon, string stanowisko)
            : base(id, imie, nazwisko, telefon)
        {
            Stanowisko = stanowisko;
        }

        public override string PobierzOpis()
        {
            return $"Pracownik {Id}: {Imie} {Nazwisko}, stanowisko: {Stanowisko}, tel.: {Telefon}";
        }
    }
}
