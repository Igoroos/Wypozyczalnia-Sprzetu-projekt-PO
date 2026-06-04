using System;

namespace WypozyczalniaSprzetu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== System Wypożyczalni Sprzętu ===");

            Sprzet narty = new Sprzet();
            narty.Nazwa = "Narty zjazdowe";
            narty.Typ = "Sprzęt sportowy";
            narty.Rozmiar = "170 cm";
            narty.Marka = "Fischer";
            narty.CenaBazowa = 50.00m;
            narty.StanTechniczny = StanSprzetu.Idealny; 
            
            narty.OznaczJakoDostepny();

            PozycjaWypozyczenia pozycja1 = new PozycjaWypozyczenia();
            pozycja1.PowiazanySprzet = narty;
            pozycja1.Ilosc = 1;
            pozycja1.CenaZaDobe = narty.CenaBazowa;

            int liczbaDni = 3;
            decimal koszt = pozycja1.ObliczKosztPozycji(liczbaDni);

            Console.WriteLine("Szczegóły Wypożyczenia:");
            Console.WriteLine($"Sprzęt: {pozycja1.PowiazanySprzet.Nazwa} ({pozycja1.PowiazanySprzet.Marka})");
            Console.WriteLine($"Stan techniczny przed wydaniem: {pozycja1.PowiazanySprzet.StanTechniczny}");
            Console.WriteLine($"Koszt wypożyczenia na {liczbaDni} dni: {koszt} zł");
            Console.WriteLine("===================================");
        }
    }
}