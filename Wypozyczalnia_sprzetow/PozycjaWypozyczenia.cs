using System;

namespace WypozyczalniaSprzetu
{
    public class PozycjaWypozyczenia
    {
        public int Ilosc { get; set; }
        public decimal CenaZaDobe { get; set; }
        public Sprzet PowiazanySprzet { get; set; }

        public decimal ObliczKosztPozycji(int liczbaDni)
        {
            return Ilosc * CenaZaDobe * liczbaDni;
        }
    }
}