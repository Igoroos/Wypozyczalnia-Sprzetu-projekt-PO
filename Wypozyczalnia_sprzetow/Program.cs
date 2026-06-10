using System;
using System.Collections.Generic;
using System.Linq;
using WypozyczalniaSprzetuGorskiego.Data;
using WypozyczalniaSprzetuGorskiego.Models;

namespace WypozyczalniaSprzetuGorskiego
{
    internal class Program
    {
        private static DaneWypozyczalni dane = new DaneWypozyczalni();
        private const string SciezkaPliku = "dane.json";

        private static void Main(string[] args)
        {
            dane = MenedzerPlikow.WczytajZPliku(SciezkaPliku);
            
            // Jeśli plik nie istniał (baza jest pusta), ładujemy dane testowe z kodu
            if (dane.Kategorie.Count == 0 && dane.Sprzety.Count == 0)
            {
                dane = PrzygotujDaneStartowe();
            }

            bool dziala = true;

            while (dziala)
            {
                InterfejsKonsolowy.PokazMenu();
                string? wybor = Console.ReadLine();

                try
                {
                    switch (wybor)
                    {
                        case "1": PokazSprzet(); break;
                        case "2": PokazKlientow(); break;
                        case "3": PokazPracownikow(); break;
                        case "4": PokazWypozyczenia(); break;
                        case "5": PokazPlatnosci(); break;
                        case "6": ZapiszDane(); break;
                        case "7": WczytajDane(); break;
                        case "8": DodajSprzetInteraktywnie(); break;
                        case "9": TestujPromocje(); break;
                        case "10": DodajWypozyczenieInteraktywnie(); break;
                        case "11": ZwrocSprzetInteraktywnie(); break;
                        case "0": dziala = false; break;
                        default:
                            InterfejsKonsolowy.PokazBlad("Nieprawidłowa opcja.");
                            InterfejsKonsolowy.CzekajNaKlawisz();
                            break;
                    }
                }
                catch (FormatException)
                {
                    
                    InterfejsKonsolowy.PokazBlad("Błąd: Wprowadzono tekst tam, gdzie oczekiwano liczby!");
                    InterfejsKonsolowy.CzekajNaKlawisz();
                }
                catch (Exception ex)
                {
                
                    InterfejsKonsolowy.PokazBlad($"Wystąpił błąd systemu: {ex.Message}");
                    InterfejsKonsolowy.CzekajNaKlawisz();
                }
            }

            Console.Clear();
            Console.WriteLine("Zamknięto program.");
        }

       
        private static void DodajSprzetInteraktywnie()
        {
            InterfejsKonsolowy.PokazNaglowek("DODAWANIE NOWEGO SPRZĘTU (Test Błędów)");

            Console.Write("Podaj nazwę sprzętu: ");
            string nazwa = Console.ReadLine() ?? "Brak nazwy";

            Console.Write("Podaj markę: ");
            string marka = Console.ReadLine() ?? "Brak marki";

           
            Console.Write("Podaj cenę za dobę (np. 45,50): ");
            decimal cena = decimal.Parse(Console.ReadLine() ?? "0");

            int noweId = dane.Sprzety.Count > 0 ? dane.Sprzety.Max(s => s.Id) + 1 : 1;
            Sprzet nowySprzet = new Sprzet(noweId, nazwa, TypSprzetu.Inny, cena, "uniwersalny", marka, StanTechniczny.BardzoDobry);
            
            dane.Sprzety.Add(nowySprzet);
            
            InterfejsKonsolowy.PokazSukces($"Sprzęt '{nazwa}' został dodany pomyślnie!");
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static void TestujPromocje()
        {
            InterfejsKonsolowy.PokazNaglowek("KALKULATOR PROMOCJI (Test Interfejsów i Polimorfizmu)");

            Console.Write("Podaj kwotę wypożyczenia do symulacji (np. 150): ");
            decimal kwota = decimal.Parse(Console.ReadLine() ?? "0");

            
            List<IPromocja> dostepnePromocje = new List<IPromocja>
            {
                new PromocjaWeekendowa(),
                new PromocjaStalyKlient()
            };

            Console.WriteLine($"\nSymulacja zniżek dla kwoty: {kwota:C}\n");

            foreach (IPromocja promocja in dostepnePromocje)
            {
                decimal rabat = promocja.ObliczRabat(kwota);
                Console.WriteLine($"- {promocja.NazwaPromocji}");
                Console.WriteLine($"  Zniżka: {rabat:C}. Do zapłaty po rabacie: {kwota - rabat:C}\n");
            }

            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static void DodajWypozyczenieInteraktywnie()
        {
            InterfejsKonsolowy.PokazNaglowek("NOWE WYPOŻYCZENIE");

            if (dane.Klienci.Count == 0 || dane.Pracownicy.Count == 0)
            {
                throw new Exception("Brak klientów lub pracowników w bazie. Zresetuj plik JSON, by załadować dane startowe.");
            }

            Console.Write("Podaj ID sprzętu, który chcesz wypożyczyć: ");
            int idSprzetu = int.Parse(Console.ReadLine() ?? "0");
            Sprzet? sprzet = dane.Sprzety.FirstOrDefault(s => s.Id == idSprzetu);

            if (sprzet == null) throw new Exception("Nie znaleziono sprzętu o podanym ID.");
            if (!sprzet.SprawdzDostepnosc()) throw new Exception("Ten sprzęt jest uszkodzony lub już wypożyczony!");

            Console.Write("Na ile dni chcesz wypożyczyć sprzęt? ");
            int dni = int.Parse(Console.ReadLine() ?? "0");
            if (dni <= 0) throw new Exception("Liczba dni musi być większa od zera.");

           
            Klient? klient = InterfejsKonsolowy.WybierzKlienta(dane);
            if (klient == null)
            {
                InterfejsKonsolowy.CzekajNaKlawisz();
                return;
            }
            Pracownik pracownik = dane.Pracownicy[0];

            int noweIdWypozyczenia = dane.Wypozyczenia.Count > 0 ? dane.Wypozyczenia.Max(w => w.Id) + 1 : 1;
            Wypozyczenie noweWypozyczenie = new Wypozyczenie(noweIdWypozyczenia, klient, pracownik);
            
            
            noweWypozyczenie.DodajPozycje(new PozycjaWypozyczenia(1, sprzet));
            noweWypozyczenie.Rozpocznij(DateTime.Now, DateTime.Now.AddDays(dni));

            
            int noweIdPlatnosci = dane.Platnosci.Count > 0 ? dane.Platnosci.Max(p => p.Id) + 1 : 1;
            Platnosc nowaPlatnosc = noweWypozyczenie.UtworzPlatnosc(noweIdPlatnosci, MetodaPlatnosci.Karta);
            
            dane.Wypozyczenia.Add(noweWypozyczenie);
            dane.Platnosci.Add(nowaPlatnosc);

            InterfejsKonsolowy.PokazSukces($"Wypożyczono sprzęt! Koszt: {noweWypozyczenie.KosztCalkowity:C}. Została utworzona płatność.");
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static void ZwrocSprzetInteraktywnie()
        {
            InterfejsKonsolowy.PokazNaglowek("ZWROT SPRZĘTU");

            Console.Write("Podaj ID wypożyczenia do zwrotu: ");
            int idWypozyczenia = int.Parse(Console.ReadLine() ?? "0");

            Wypozyczenie? wypozyczenie = dane.Wypozyczenia.FirstOrDefault(w => w.Id == idWypozyczenia);

            if (wypozyczenie == null) throw new Exception("Nie znaleziono wypożyczenia o podanym ID.");
            if (wypozyczenie.Status != StatusWypozyczenia.Aktywne) throw new Exception("To wypożyczenie nie jest aktywne (zostało już zakończone lub anulowane).");

            wypozyczenie.Zakoncz(DateTime.Now);

            if (wypozyczenie.Platnosc != null)
            {
                wypozyczenie.Platnosc.Zatwierdz();
            }

            InterfejsKonsolowy.PokazSukces("Sprzęt został poprawnie zwrócony, a płatność zatwierdzona.");
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

       
        private static void PokazSprzet()
        {
            InterfejsKonsolowy.PokazNaglowek("LISTA SPRZĘTU");
            foreach (Sprzet sprzet in dane.Sprzety)
            {
                Console.WriteLine(sprzet);
            }
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static void PokazKlientow()
        {
            InterfejsKonsolowy.PokazNaglowek("LISTA KLIENTÓW");
            foreach (Klient klient in dane.Klienci)
            {
                Console.WriteLine(klient.PobierzOpis());
            }
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static void PokazPracownikow()
        {
            InterfejsKonsolowy.PokazNaglowek("LISTA PRACOWNIKÓW");
            foreach (Pracownik pracownik in dane.Pracownicy)
            {
                Console.WriteLine(pracownik.PobierzOpis());
            }
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static void PokazWypozyczenia()
        {
            InterfejsKonsolowy.PokazNaglowek("LISTA WYPOŻYCZEŃ");
            foreach (Wypozyczenie wypozyczenie in dane.Wypozyczenia)
            {
                Console.WriteLine(wypozyczenie);
                foreach (PozycjaWypozyczenia pozycja in wypozyczenie.Pozycje)
                {
                    Console.WriteLine($"   - {pozycja}");
                }
                Console.WriteLine();
            }
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static void PokazPlatnosci()
        {
            InterfejsKonsolowy.PokazNaglowek("LISTA PŁATNOŚCI");
            foreach (Platnosc platnosc in dane.Platnosci)
            {
                Console.WriteLine(platnosc);
            }
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static void ZapiszDane()
        {
            InterfejsKonsolowy.PokazNaglowek("ZAPIS DANYCH");
            MenedzerPlikow.ZapiszDoPliku(SciezkaPliku, dane);
            InterfejsKonsolowy.PokazSukces("Dane zapisano do pliku dane.json.");
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static void WczytajDane()
        {
            InterfejsKonsolowy.PokazNaglowek("ODCZYT DANYCH");
            dane = MenedzerPlikow.WczytajZPliku(SciezkaPliku);
            InterfejsKonsolowy.PokazSukces("Dane wczytano z pliku dane.json.");
            InterfejsKonsolowy.CzekajNaKlawisz();
        }

        private static DaneWypozyczalni PrzygotujDaneStartowe()
        {
            DaneWypozyczalni noweDane = new DaneWypozyczalni();

            noweDane.Klienci.Add(new Klient(1, "Jan", "Kowalski", "123456789", "ABC123456"));
            noweDane.Pracownicy.Add(new Pracownik(1, "Anna", "Nowak", "987654321", "Sprzedawca"));

            noweDane.Sprzety.Add(new Sprzet(1, "Narty Atomic Redster", TypSprzetu.Narty, 60m, "170 cm", "Atomic", StanTechniczny.BardzoDobry));
            noweDane.Sprzety.Add(new Sprzet(2, "Snowboard Burton Custom", TypSprzetu.Snowboard, 70m, "158 cm", "Burton", StanTechniczny.BardzoDobry));
            noweDane.Sprzety.Add(new Sprzet(3, "Kask Uvex", TypSprzetu.Kask, 20m, "M", "Uvex", StanTechniczny.BardzoDobry));

            return noweDane;
        }
    }
}