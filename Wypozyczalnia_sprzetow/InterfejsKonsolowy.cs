using WypozyczalniaSprzetuGorskiego.Data;
using WypozyczalniaSprzetuGorskiego.Models;

namespace WypozyczalniaSprzetuGorskiego
{
    public static class InterfejsKonsolowy
    {
        public static void PokazNaglowek(string tytul)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("==============================================");
            Console.WriteLine("     WYPOŻYCZALNIA SPRZĘTU GÓRSKIEGO");
            Console.WriteLine("==============================================");

            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(tytul);
            Console.ResetColor();
            Console.WriteLine("----------------------------------------------");
        }

        public static void PokazMenu()
        {
            PokazNaglowek("MENU GŁÓWNE");

            Console.WriteLine("1. Wyświetl sprzęt");
            Console.WriteLine("2. Wyświetl klientów");
            Console.WriteLine("3. Wyświetl pracowników");
            Console.WriteLine("4. Wyświetl wypożyczenia");
            Console.WriteLine("5. Wyświetl płatności");
            Console.WriteLine("6. Zapisz dane do pliku");
            Console.WriteLine("7. Wczytaj dane z pliku");
            Console.WriteLine("8. Dodaj nowy sprzęt (Test obsługi błędów)");
            Console.WriteLine("9. Kalkulator zniżek (Test interfejsów)");
            Console.WriteLine("10. Wypożycz sprzęt");
            Console.WriteLine("11. Zwróć sprzęt");
            Console.WriteLine("0. Wyjście");

            Console.WriteLine();
            Console.Write("Wybierz opcję: ");
        }
        public static Klient? WybierzKlienta(DaneWypozyczalni dane)
        {
            PokazNaglowek("WYBÓR KLIENTA");

            if (dane.Klienci.Count == 0)
            {
                PokazBlad("Brak klientów w systemie.");
                return null;
            }

            Console.WriteLine("Dostępni klienci:");
            Console.WriteLine();

            foreach (Klient klient in dane.Klienci)
            {
                Console.WriteLine($"{klient.Id}. {klient.Imie} {klient.Nazwisko}, tel.: {klient.Telefon}");
            }

            Console.WriteLine();
            Console.Write("Podaj ID klienta, który wypożycza sprzęt: ");

            string? tekst = Console.ReadLine();

            if (!int.TryParse(tekst, out int idKlienta))
            {
                PokazBlad("Podano nieprawidłowe ID klienta.");
                return null;
            }

            Klient? wybranyKlient = dane.Klienci.FirstOrDefault(k => k.Id == idKlienta);

            if (wybranyKlient == null)
            {
                PokazBlad("Nie znaleziono klienta o podanym ID.");
                return null;
            }

            PokazSukces($"Wybrano klienta: {wybranyKlient.Imie} {wybranyKlient.Nazwisko}");
            return wybranyKlient;
        }
        public static void PokazSukces(string komunikat)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(komunikat);
            Console.ResetColor();
        }

        public static void PokazBlad(string komunikat)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(komunikat);
            Console.ResetColor();
        }

        public static void PokazInfo(string komunikat)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(komunikat);
            Console.ResetColor();
        }

        public static void CzekajNaKlawisz()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}