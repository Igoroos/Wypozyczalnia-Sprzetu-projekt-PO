using System.Text.Json.Serialization;

namespace WypozyczalniaSprzetuGorskiego.Models
{
    public class PozycjaWypozyczenia
    {
        public int Id { get; set; }
        public int SprzetId { get; set; }

        [JsonIgnore]
        public Sprzet? Sprzet { get; set; }

        public int Ilosc { get; set; } = 1;

        public PozycjaWypozyczenia()
        {
        }

        public PozycjaWypozyczenia(int id, Sprzet sprzet, int ilosc = 1)
        {
            if (sprzet == null)
            {
                throw new ArgumentNullException(nameof(sprzet));
            }

            if (ilosc <= 0)
            {
                throw new ArgumentException("Ilość musi być większa od zera.");
            }

            Id = id;
            Sprzet = sprzet;
            SprzetId = sprzet.Id;
            Ilosc = ilosc;
        }

        public decimal ObliczKosztPozycji(int liczbaDni)
        {
            if (Sprzet == null)
            {
                throw new InvalidOperationException("Nie można obliczyć kosztu bez przypisanego sprzętu.");
            }

            if (liczbaDni <= 0)
            {
                throw new ArgumentException("Liczba dni musi być większa od zera.");
            }

            return Sprzet.CenaZaDobe * Ilosc * liczbaDni;
        }

        public override string ToString()
        {
            string nazwa = Sprzet != null ? Sprzet.Nazwa : $"Sprzęt ID: {SprzetId}";
            return $"{nazwa}, ilość: {Ilosc}";
        }
    }
}
