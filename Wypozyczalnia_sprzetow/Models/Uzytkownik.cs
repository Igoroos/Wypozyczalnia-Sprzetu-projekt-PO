namespace WypozyczalniaSprzetuGorskiego.Models
{
    public abstract class Uzytkownik
    {
        public int Id { get; set; }
        public string Imie { get; set; } = string.Empty;
        public string Nazwisko { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;

        protected Uzytkownik()
        {
        }

        protected Uzytkownik(int id, string imie, string nazwisko, string telefon)
        {
            Id = id;
            Imie = imie;
            Nazwisko = nazwisko;
            Telefon = telefon;
        }

        public abstract string PobierzOpis();

        public virtual string PobierzDaneKontaktowe()
        {
            return $"{Imie} {Nazwisko}, tel.: {Telefon}";
        }

        public override string ToString()
        {
            return PobierzOpis();
        }
    }
}
