using System.Text.Json;
using System.Text.Json.Serialization;
using WypozyczalniaSprzetuGorskiego.Models;

namespace WypozyczalniaSprzetuGorskiego.Data
{
    public static class MenedzerPlikow
    {
        private static readonly JsonSerializerOptions OpcjeJson = new JsonSerializerOptions
        {
            WriteIndented = true
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
            DaneWypozyczalni dane = JsonSerializer.Deserialize<DaneWypozyczalni>(json, OpcjeJson) ?? new DaneWypozyczalni();

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
                KategoriaSprzetu? kategoria = dane.Kategorie.FirstOrDefault(k => k.Id == sprzet.KategoriaId);
                if (kategoria != null)
                {
                    kategoria.DodajSprzet(sprzet);
                }
            }

            foreach (Wypozyczenie wypozyczenie in dane.Wypozyczenia)
            {
                wypozyczenie.Klient = dane.Klienci.FirstOrDefault(k => k.Id == wypozyczenie.KlientId);
                wypozyczenie.Pracownik = dane.Pracownicy.FirstOrDefault(p => p.Id == wypozyczenie.PracownikId);
                wypozyczenie.Platnosc = dane.Platnosci.FirstOrDefault(p => p.WypozyczenieId == wypozyczenie.Id);

                foreach (PozycjaWypozyczenia pozycja in wypozyczenie.Pozycje)
                {
                    pozycja.Sprzet = dane.Sprzety.FirstOrDefault(s => s.Id == pozycja.SprzetId);
                }
            }
        }
    }
}
