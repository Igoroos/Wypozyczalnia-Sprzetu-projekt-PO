using WypozyczalniaSprzetuGorskiego.Data;
using WypozyczalniaSprzetuGorskiego.Models;

namespace WypozyczalniaSprzetuGorskiego
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DaneWypozyczalni dane = PrzygotujDaneStartowe();

            Console.WriteLine("=== WYPOŻYCZALNIA SPRZĘTU GÓRSKIEGO ===");
            Console.WriteLine();

            Console.WriteLine("DANE STARTOWE:");
            Console.WriteLine($"Liczba kategorii: {dane.Kategorie.Count}");
            Console.WriteLine($"Liczba sprzętów: {dane.Sprzety.Count}");
            Console.WriteLine();

            Console.WriteLine("LISTA SPRZĘTU PRZED WYPOŻYCZENIAMI:");
            WyswietlSprzet(dane);
            Console.WriteLine();

            // WYPOŻYCZENIE 1
            Wypozyczenie wypozyczenie1 = new Wypozyczenie(1, 1001);
            wypozyczenie1.DodajPozycje(new PozycjaWypozyczenia(1, dane.Sprzety[0]));
            wypozyczenie1.DodajPozycje(new PozycjaWypozyczenia(2, dane.Sprzety[4]));
            wypozyczenie1.Rozpocznij(DateTime.Today, DateTime.Today.AddDays(3));
            wypozyczenie1.Zakoncz(DateTime.Today.AddDays(3));

            Platnosc platnosc1 = wypozyczenie1.UtworzPlatnosc(1, MetodaPlatnosci.Karta);
            platnosc1.Zatwierdz();

            dane.Wypozyczenia.Add(wypozyczenie1);
            dane.Platnosci.Add(platnosc1);

            // WYPOŻYCZENIE 2
            Wypozyczenie wypozyczenie2 = new Wypozyczenie(2, 1002);
            wypozyczenie2.DodajPozycje(new PozycjaWypozyczenia(3, dane.Sprzety[1]));
            wypozyczenie2.DodajPozycje(new PozycjaWypozyczenia(4, dane.Sprzety[5]));
            wypozyczenie2.Rozpocznij(DateTime.Today.AddDays(1), DateTime.Today.AddDays(5));
            wypozyczenie2.Zakoncz(DateTime.Today.AddDays(6));

            Platnosc platnosc2 = wypozyczenie2.UtworzPlatnosc(2, MetodaPlatnosci.Blik);
            platnosc2.Zatwierdz();

            dane.Wypozyczenia.Add(wypozyczenie2);
            dane.Platnosci.Add(platnosc2);

            // WYPOŻYCZENIE 3 - aktywne, jeszcze niezakończone
            Wypozyczenie wypozyczenie3 = new Wypozyczenie(3, 1003);
            wypozyczenie3.DodajPozycje(new PozycjaWypozyczenia(5, dane.Sprzety[2]));
            wypozyczenie3.DodajPozycje(new PozycjaWypozyczenia(6, dane.Sprzety[7]));
            wypozyczenie3.Rozpocznij(DateTime.Today, DateTime.Today.AddDays(2));

            dane.Wypozyczenia.Add(wypozyczenie3);

            Console.WriteLine("WYPOŻYCZENIA PO UTWORZENIU:");
            WyswietlWypozyczenia(dane);
            Console.WriteLine();

            Console.WriteLine("PŁATNOŚCI:");
            WyswietlPlatnosci(dane);
            Console.WriteLine();

            Console.WriteLine("LISTA SPRZĘTU PO WYPOŻYCZENIACH:");
            WyswietlSprzet(dane);
            Console.WriteLine();

            // ZAPIS DO PLIKU
            MenedzerPlikow.ZapiszDoPliku("dane.json", dane);
            Console.WriteLine("Dane zapisano do pliku dane.json.");
            Console.WriteLine();

            // ODCZYT Z PLIKU
            DaneWypozyczalni daneWczytane = MenedzerPlikow.WczytajZPliku("dane.json");

            Console.WriteLine("DANE WCZYTANE Z PLIKU:");
            Console.WriteLine($"Liczba kategorii: {daneWczytane.Kategorie.Count}");
            Console.WriteLine($"Liczba sprzętów: {daneWczytane.Sprzety.Count}");
            Console.WriteLine($"Liczba wypożyczeń: {daneWczytane.Wypozyczenia.Count}");
            Console.WriteLine($"Liczba płatności: {daneWczytane.Platnosci.Count}");
            Console.WriteLine();

            Console.WriteLine("SPRZĘT PO WCZYTANIU:");
            WyswietlSprzet(daneWczytane);
            Console.WriteLine();

            Console.WriteLine("WYPOŻYCZENIA PO WCZYTANIU:");
            WyswietlWypozyczenia(daneWczytane);
            Console.WriteLine();

            Console.WriteLine("Koniec programu.");
        }

        private static DaneWypozyczalni PrzygotujDaneStartowe()
        {
            DaneWypozyczalni dane = new DaneWypozyczalni();

            KategoriaSprzetu kategoriaNarty = new KategoriaSprzetu(
                1,
                "Narty",
                "Sprzęt narciarski do jazdy po przygotowanych stokach."
            );

            KategoriaSprzetu kategoriaSnowboard = new KategoriaSprzetu(
                2,
                "Snowboard",
                "Deski snowboardowe i sprzęt do jazdy freestyle."
            );

            KategoriaSprzetu kategoriaAkcesoria = new KategoriaSprzetu(
                3,
                "Akcesoria ochronne",
                "Kaski, kijki i podstawowe akcesoria bezpieczeństwa."
            );

            KategoriaSprzetu kategoriaTurystyka = new KategoriaSprzetu(
                4,
                "Turystyka górska",
                "Sprzęt do pieszych wycieczek górskich."
            );

            Sprzet sprzet1 = new Sprzet(
                1,
                "Narty Atomic Redster",
                TypSprzetu.Narty,
                60m,
                "170 cm",
                "Atomic",
                StanTechniczny.BardzoDobry
            );

            Sprzet sprzet2 = new Sprzet(
                2,
                "Narty Fischer RC4",
                TypSprzetu.Narty,
                55m,
                "165 cm",
                "Fischer",
                StanTechniczny.Dobry
            );

            Sprzet sprzet3 = new Sprzet(
                3,
                "Snowboard Burton Custom",
                TypSprzetu.Snowboard,
                70m,
                "158 cm",
                "Burton",
                StanTechniczny.BardzoDobry
            );

            Sprzet sprzet4 = new Sprzet(
                4,
                "Snowboard Head True",
                TypSprzetu.Snowboard,
                50m,
                "155 cm",
                "Head",
                StanTechniczny.Dobry
            );

            Sprzet sprzet5 = new Sprzet(
                5,
                "Kask Uvex",
                TypSprzetu.Kask,
                20m,
                "M",
                "Uvex",
                StanTechniczny.BardzoDobry
            );

            Sprzet sprzet6 = new Sprzet(
                6,
                "Kijki Leki",
                TypSprzetu.Kijki,
                15m,
                "120 cm",
                "Leki",
                StanTechniczny.Dobry
            );

            Sprzet sprzet7 = new Sprzet(
                7,
                "Raki Climbing Technology",
                TypSprzetu.Raki,
                35m,
                "uniwersalny",
                "Climbing Technology",
                StanTechniczny.Dobry
            );

            Sprzet sprzet8 = new Sprzet(
                8,
                "Czekan Black Diamond",
                TypSprzetu.Czekan,
                40m,
                "60 cm",
                "Black Diamond",
                StanTechniczny.BardzoDobry
            );

            Sprzet sprzet9 = new Sprzet(
                9,
                "Plecak Deuter 35L",
                TypSprzetu.Plecak,
                25m,
                "35 L",
                "Deuter",
                StanTechniczny.Dobry
            );

            Sprzet sprzet10 = new Sprzet(
                10,
                "Buty narciarskie Salomon",
                TypSprzetu.ButyNarciarskie,
                45m,
                "42",
                "Salomon",
                StanTechniczny.Dobry
            );

            kategoriaNarty.DodajSprzet(sprzet1);
            kategoriaNarty.DodajSprzet(sprzet2);
            kategoriaNarty.DodajSprzet(sprzet10);

            kategoriaSnowboard.DodajSprzet(sprzet3);
            kategoriaSnowboard.DodajSprzet(sprzet4);

            kategoriaAkcesoria.DodajSprzet(sprzet5);
            kategoriaAkcesoria.DodajSprzet(sprzet6);

            kategoriaTurystyka.DodajSprzet(sprzet7);
            kategoriaTurystyka.DodajSprzet(sprzet8);
            kategoriaTurystyka.DodajSprzet(sprzet9);

            dane.Kategorie.Add(kategoriaNarty);
            dane.Kategorie.Add(kategoriaSnowboard);
            dane.Kategorie.Add(kategoriaAkcesoria);
            dane.Kategorie.Add(kategoriaTurystyka);

            dane.Sprzety.Add(sprzet1);
            dane.Sprzety.Add(sprzet2);
            dane.Sprzety.Add(sprzet3);
            dane.Sprzety.Add(sprzet4);
            dane.Sprzety.Add(sprzet5);
            dane.Sprzety.Add(sprzet6);
            dane.Sprzety.Add(sprzet7);
            dane.Sprzety.Add(sprzet8);
            dane.Sprzety.Add(sprzet9);
            dane.Sprzety.Add(sprzet10);

            return dane;
        }

        private static void WyswietlSprzet(DaneWypozyczalni dane)
        {
            foreach (Sprzet sprzet in dane.Sprzety)
            {
                Console.WriteLine(sprzet);
            }
        }

        private static void WyswietlWypozyczenia(DaneWypozyczalni dane)
        {
            foreach (Wypozyczenie wypozyczenie in dane.Wypozyczenia)
            {
                Console.WriteLine(wypozyczenie);

                foreach (PozycjaWypozyczenia pozycja in wypozyczenie.Pozycje)
                {
                    Console.WriteLine($"  - {pozycja}");
                }

                if (wypozyczenie.SprawdzOpoznienie())
                {
                    Console.WriteLine("  Uwaga: wypożyczenie jest opóźnione.");
                }
            }
        }

        private static void WyswietlPlatnosci(DaneWypozyczalni dane)
        {
            foreach (Platnosc platnosc in dane.Platnosci)
            {
                Console.WriteLine(platnosc);
            }
        }
    }
}