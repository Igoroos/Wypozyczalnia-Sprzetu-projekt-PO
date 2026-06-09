using System.Text.Json;
using System.Text.Json.Serialization;
using WypozyczalniaSprzetuGorskiego.Models;

namespace WypozyczalniaSprzetuGorskiego.Data
{
    public static class MenedzerPlikow
    {
        private static readonly JsonSerializerOptions OpcjeJson = new JsonSerializerOptions
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        static MenedzerPlikow()
        {
            OpcjeJson.Converters.Add(new JsonStringEnumConverter());
        }

        public static void ZapiszDoPliku(string sciezka, DaneWypozyczalni dane)
        {
            string json = JsonSerializer.Serialize(dane, OpcjeJson);
            File.WriteAllText(sciezka, json);
        }

        public static DaneWypozyczalni WczytajZPliku(string sciezka)
        {
            if (!File.Exists(sciezka))
            {
                return new DaneWypozyczalni();
            }

            string json = File.ReadAllText(sciezka);

            DaneWypozyczalni dane = JsonSerializer.Deserialize<DaneWypozyczalni>(json, OpcjeJson)
                                      ?? new DaneWypozyczalni();

            NaprawRelacje(dane);

            return dane;
        }

        private static void NaprawRelacje(DaneWypozyczalni dane)
        {
            foreach (KategoriaSprzetu kategoria in dane.Kategorie)
            {
                kategoria.Sprzety.Clear();
            }

            foreach (Sprzet sprzet in dane.Sprzety)
            {
                KategoriaSprzetu? kategoria = dane.Kategorie
                    .FirstOrDefault(k => k.Id == sprzet.KategoriaId);

                if (kategoria != null)
                {
                    sprzet.Kategoria = kategoria;
                    kategoria.Sprzety.Add(sprzet);
                }
            }

            foreach (Wypozyczenie wypozyczenie in dane.Wypozyczenia)
            {
                foreach (PozycjaWypozyczenia pozycja in wypozyczenie.Pozycje)
                {
                    Sprzet? sprzet = dane.Sprzety
                        .FirstOrDefault(s => s.Id == pozycja.SprzetId);

                    if (sprzet != null)
                    {
                        pozycja.Sprzet = sprzet;
                    }
                }

                Platnosc? platnosc = dane.Platnosci
                    .FirstOrDefault(p => p.WypozyczenieId == wypozyczenie.Id);

                wypozyczenie.Platnosc = platnosc;
            }
        }
    }
}