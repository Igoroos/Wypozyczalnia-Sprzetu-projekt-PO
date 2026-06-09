using System.Text.Json.Serialization;

namespace WypozyczalniaSprzetuGorskiego.Models
{
    public enum TypSprzetu
    {
        Narty,
        Snowboard,
        ButyNarciarskie,
        Kask,
        Kijki,
        Raki,
        Czekan,
        Plecak,
        Inny
    }

    public enum StanTechniczny
    {
        BardzoDobry,
        Dobry,
        DoSerwisu,
        Uszkodzony
    }

    public class Sprzet
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public TypSprzetu Typ { get; set; }
        public decimal CenaZaDobe { get; set; }
        public string Rozmiar { get; set; } = string.Empty;
        public string Marka { get; set; } = string.Empty;
        public bool Dostepny { get; set; } = true;
        public StanTechniczny StanTechniczny { get; set; } = StanTechniczny.Dobry;
        public int KategoriaId { get; set; }

        [JsonIgnore]
        public KategoriaSprzetu? Kategoria { get; set; }

        public Sprzet()
        {
        }

        public Sprzet(int id, string nazwa, TypSprzetu typ, decimal cenaZaDobe, string rozmiar, string marka, StanTechniczny stanTechniczny)
        {
            if (cenaZaDobe <= 0)
            {
                throw new ArgumentException("Cena za dobę musi być większa od zera.");
            }

            Id = id;
            Nazwa = nazwa;
            Typ = typ;
            CenaZaDobe = cenaZaDobe;
            Rozmiar = rozmiar;
            Marka = marka;
            StanTechniczny = stanTechniczny;
            Dostepny = true;
        }

        public bool SprawdzDostepnosc()
        {
            return Dostepny && StanTechniczny != StanTechniczny.Uszkodzony && StanTechniczny != StanTechniczny.DoSerwisu;
        }

        public void OznaczJakoWypozyczony()
        {
            if (!SprawdzDostepnosc())
            {
                throw new InvalidOperationException($"Sprzęt '{Nazwa}' nie jest dostępny do wypożyczenia.");
            }

            Dostepny = false;
        }

        public void OznaczJakoDostepny()
        {
            if (StanTechniczny == StanTechniczny.Uszkodzony || StanTechniczny == StanTechniczny.DoSerwisu)
            {
                Dostepny = false;
                return;
            }

            Dostepny = true;
        }

        public void ZmienStanTechniczny(StanTechniczny nowyStan)
        {
            StanTechniczny = nowyStan;

            if (nowyStan == StanTechniczny.Uszkodzony || nowyStan == StanTechniczny.DoSerwisu)
            {
                Dostepny = false;
            }
        }

        public override string ToString()
        {
            string dostepnosc = Dostepny ? "dostępny" : "niedostępny";
            return $"{Id}. {Nazwa}, {Typ}, {Marka}, rozmiar: {Rozmiar}, cena: {CenaZaDobe} zł/doba, stan: {StanTechniczny}, {dostepnosc}";
        }
    }
}
