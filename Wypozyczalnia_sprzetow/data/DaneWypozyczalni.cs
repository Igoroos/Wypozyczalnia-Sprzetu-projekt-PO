using WypozyczalniaSprzetuGorskiego.Models;

namespace WypozyczalniaSprzetuGorskiego.Data
{
    public class DaneWypozyczalni
    {
        public List<Klient> Klienci { get; set; } = new List<Klient>();
        public List<Pracownik> Pracownicy { get; set; } = new List<Pracownik>();
        public List<KategoriaSprzetu> Kategorie { get; set; } = new List<KategoriaSprzetu>();
        public List<Sprzet> Sprzety { get; set; } = new List<Sprzet>();
        public List<Wypozyczenie> Wypozyczenia { get; set; } = new List<Wypozyczenie>();
        public List<Platnosc> Platnosci { get; set; } = new List<Platnosc>();
    }
}
