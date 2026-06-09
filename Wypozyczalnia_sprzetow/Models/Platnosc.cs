namespace WypozyczalniaSprzetuGorskiego.Models
{
    public enum MetodaPlatnosci
    {
        Gotowka,
        Karta,
        Blik,
        Przelew
    }

    public enum StatusPlatnosci
    {
        Oczekujaca,
        Zatwierdzona,
        Anulowana,
        Blad
    }

    public class Platnosc
    {
        public int Id { get; set; }
        public int WypozyczenieId { get; set; }
        public decimal Kwota { get; set; }
        public MetodaPlatnosci MetodaPlatnosci { get; set; }
        public StatusPlatnosci Status { get; set; } = StatusPlatnosci.Oczekujaca;
        public DateTime? DataPlatnosci { get; set; }

        public Platnosc()
        {
        }

        public Platnosc(int id, int wypozyczenieId, decimal kwota, MetodaPlatnosci metodaPlatnosci)
        {
            Id = id;
            WypozyczenieId = wypozyczenieId;
            Kwota = kwota;
            MetodaPlatnosci = metodaPlatnosci;
            Status = StatusPlatnosci.Oczekujaca;
        }

        public void Zatwierdz()
        {
            if (Kwota <= 0)
            {
                Status = StatusPlatnosci.Blad;
                throw new InvalidOperationException("Nie można zatwierdzić płatności z kwotą mniejszą lub równą zero.");
            }

            if (Status == StatusPlatnosci.Anulowana)
            {
                throw new InvalidOperationException("Nie można zatwierdzić anulowanej płatności.");
            }

            Status = StatusPlatnosci.Zatwierdzona;
            DataPlatnosci = DateTime.Now;
        }

        public void Anuluj()
        {
            if (Status == StatusPlatnosci.Zatwierdzona)
            {
                throw new InvalidOperationException("Nie można anulować zatwierdzonej płatności.");
            }

            Status = StatusPlatnosci.Anulowana;
        }

        public override string ToString()
        {
            return $"Płatność {Id}, wypożyczenie: {WypozyczenieId}, kwota: {Kwota} zł, metoda: {MetodaPlatnosci}, status: {Status}";
        }
    }
}
