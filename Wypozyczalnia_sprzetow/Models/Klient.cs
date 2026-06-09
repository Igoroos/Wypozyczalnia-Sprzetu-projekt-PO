namespace WypozyczalniaSprzetuGorskiego.Models
{
    public class Klient : Uzytkownik
    {
        public string NumerDokumentu { get; set; } = string.Empty;

        public Klient()
        {
        }

        public Klient(int id, string imie, string nazwisko, string telefon, string numerDokumentu)
            : base(id, imie, nazwisko, telefon)
        {
            NumerDokumentu = numerDokumentu;
        }

        public override string PobierzOpis()
        {
            return $"Klient {Id}: {Imie} {Nazwisko}, tel.: {Telefon}, dokument: {NumerDokumentu}";
        }
    }
}
