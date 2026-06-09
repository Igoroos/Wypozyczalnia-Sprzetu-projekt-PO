using System.Text.Json.Serialization;

namespace WypozyczalniaSprzetuGorskiego.Models
{
    public enum StatusWypozyczenia
    {
        Nowe,
        Aktywne,
        Zakonczone,
        Anulowane
    }

    public class Wypozyczenie
    {
        public int Id { get; set; }
        public int KlientId { get; set; }
        public int PracownikId { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime PlanowanaDataZwrotu { get; set; }
        public DateTime? RzeczywistaDataZwrotu { get; set; }
        public StatusWypozyczenia Status { get; set; } = StatusWypozyczenia.Nowe;
        public decimal KosztCalkowity { get; set; }
        public List<PozycjaWypozyczenia> Pozycje { get; set; } = new List<PozycjaWypozyczenia>();

        [JsonIgnore]
        public Klient? Klient { get; set; }

        [JsonIgnore]
        public Pracownik? Pracownik { get; set; }

        [JsonIgnore]
        public Platnosc? Platnosc { get; set; }

        public Wypozyczenie()
        {
        }

        public Wypozyczenie(int id, Klient klient, Pracownik pracownik)
        {
            if (klient == null)
            {
                throw new ArgumentNullException(nameof(klient));
            }

            if (pracownik == null)
            {
                throw new ArgumentNullException(nameof(pracownik));
            }

            Id = id;
            Klient = klient;
            KlientId = klient.Id;
            Pracownik = pracownik;
            PracownikId = pracownik.Id;
            Status = StatusWypozyczenia.Nowe;
        }

        public void DodajPozycje(PozycjaWypozyczenia pozycja)
        {
            if (Status != StatusWypozyczenia.Nowe)
            {
                throw new InvalidOperationException("Pozycje można dodawać tylko do nowego wypożyczenia.");
            }

            if (pozycja == null)
            {
                throw new ArgumentNullException(nameof(pozycja));
            }

            if (pozycja.Sprzet == null)
            {
                throw new InvalidOperationException("Pozycja musi mieć przypisany sprzęt.");
            }

            if (!pozycja.Sprzet.SprawdzDostepnosc())
            {
                throw new InvalidOperationException($"Sprzęt '{pozycja.Sprzet.Nazwa}' nie jest dostępny.");
            }

            Pozycje.Add(pozycja);
        }

        public void Rozpocznij(DateTime dataOd, DateTime planowanaDataZwrotu)
        {
            if (Status != StatusWypozyczenia.Nowe)
            {
                throw new InvalidOperationException("Można rozpocząć tylko nowe wypożyczenie.");
            }

            if (Pozycje.Count == 0)
            {
                throw new InvalidOperationException("Nie można rozpocząć wypożyczenia bez sprzętu.");
            }

            if (planowanaDataZwrotu <= dataOd)
            {
                throw new ArgumentException("Planowana data zwrotu musi być późniejsza niż data rozpoczęcia.");
            }

            foreach (PozycjaWypozyczenia pozycja in Pozycje)
            {
                if (pozycja.Sprzet == null || !pozycja.Sprzet.SprawdzDostepnosc())
                {
                    throw new InvalidOperationException("Jeden ze sprzętów nie jest dostępny.");
                }
            }

            DataOd = dataOd;
            PlanowanaDataZwrotu = planowanaDataZwrotu;
            Status = StatusWypozyczenia.Aktywne;

            foreach (PozycjaWypozyczenia pozycja in Pozycje)
            {
                pozycja.Sprzet!.OznaczJakoWypozyczony();
            }

            ObliczKoszt();
        }

        public void Zakoncz(DateTime rzeczywistaDataZwrotu)
        {
            if (Status != StatusWypozyczenia.Aktywne)
            {
                throw new InvalidOperationException("Można zakończyć tylko aktywne wypożyczenie.");
            }

            if (rzeczywistaDataZwrotu < DataOd)
            {
                throw new ArgumentException("Data zwrotu nie może być wcześniejsza niż data rozpoczęcia.");
            }

            RzeczywistaDataZwrotu = rzeczywistaDataZwrotu;
            Status = StatusWypozyczenia.Zakonczone;

            foreach (PozycjaWypozyczenia pozycja in Pozycje)
            {
                pozycja.Sprzet?.OznaczJakoDostepny();
            }

            ObliczKoszt();
        }

        public void Anuluj()
        {
            if (Status == StatusWypozyczenia.Zakonczone)
            {
                throw new InvalidOperationException("Nie można anulować zakończonego wypożyczenia.");
            }

            if (Status == StatusWypozyczenia.Aktywne)
            {
                foreach (PozycjaWypozyczenia pozycja in Pozycje)
                {
                    pozycja.Sprzet?.OznaczJakoDostepny();
                }
            }

            Status = StatusWypozyczenia.Anulowane;
        }

        public int ObliczLiczbeDni()
        {
            DateTime dataKonca = RzeczywistaDataZwrotu ?? PlanowanaDataZwrotu;
            int liczbaDni = (int)Math.Ceiling((dataKonca - DataOd).TotalDays);
            return Math.Max(1, liczbaDni);
        }

        public decimal ObliczKoszt()
        {
            if (DataOd == default || PlanowanaDataZwrotu == default)
            {
                KosztCalkowity = 0;
                return KosztCalkowity;
            }

            int liczbaDni = ObliczLiczbeDni();
            KosztCalkowity = Pozycje.Sum(p => p.ObliczKosztPozycji(liczbaDni));
            return KosztCalkowity;
        }

        public bool SprawdzOpoznienie()
        {
            if (RzeczywistaDataZwrotu == null)
            {
                return DateTime.Now.Date > PlanowanaDataZwrotu.Date;
            }

            return RzeczywistaDataZwrotu.Value.Date > PlanowanaDataZwrotu.Date;
        }

        public Platnosc UtworzPlatnosc(int idPlatnosci, MetodaPlatnosci metodaPlatnosci)
        {
            decimal kwota = ObliczKoszt();
            Platnosc = new Platnosc(idPlatnosci, Id, kwota, metodaPlatnosci);
            return Platnosc;
        }

        public override string ToString()
        {
            string klient = Klient != null ? $"{Klient.Imie} {Klient.Nazwisko}" : $"ID {KlientId}";
            string pracownik = Pracownik != null ? $"{Pracownik.Imie} {Pracownik.Nazwisko}" : $"ID {PracownikId}";
            return $"Wypożyczenie {Id}, klient: {klient}, pracownik: {pracownik}, status: {Status}, koszt: {KosztCalkowity} zł";
        }
    }
}
