Wypożyczalnia sprzętu górskiego
Opis projektu

Projekt przedstawia aplikację konsolową napisaną w języku C#, która służy do obsługi wypożyczalni sprzętu górskiego. Program pozwala zarządzać sprzętem, klientami, pracownikami, wypożyczeniami oraz płatnościami.

Program działa w konsoli i umożliwia wykonywanie podstawowych operacji potrzebnych w prostej wypożyczalni.

Cel projektu

Celem projektu jest stworzenie prostego systemu, który pomaga w obsłudze wypożyczalni sprzętu górskiego.

Program rozwiązuje problem ręcznego prowadzenia wypożyczeń, sprawdzania dostępności sprzętu, zapisywania klientów oraz naliczania kosztów wypożyczenia. Dzięki aplikacji wszystkie dane mogą być przechowywane w jednym miejscu i zapisywane do pliku.

Główne funkcjonalności

Program umożliwia:

wyświetlanie listy sprzętu,
wyświetlanie klientów,
wyświetlanie pracowników,
wyświetlanie wypożyczeń,
wyświetlanie płatności,
dodawanie nowego sprzętu,
tworzenie wypożyczenia,
zwracanie sprzętu,
sprawdzanie dostępności sprzętu,
automatyczne oznaczanie sprzętu jako wypożyczony lub dostępny,
obliczanie kosztu wypożyczenia,
tworzenie płatności,
zatwierdzanie płatności,
zapis danych do pliku JSON,
odczyt danych z pliku JSON,
odtwarzanie relacji między obiektami po wczytaniu danych.
Struktura projektu

Projekt został podzielony na kilka plików, z których każdy odpowiada za inną część programu.

Najważniejsze pliki:

Program.cs – główny plik programu, zawiera metodę Main, menu i obsługę wyborów użytkownika.
InterfejsKonsolowy.cs – odpowiada za wygląd konsoli, menu, nagłówki i komunikaty.
Uzytkownik.cs – klasa bazowa dla użytkowników systemu.
Klient.cs – klasa reprezentująca klienta wypożyczalni.
Pracownik.cs – klasa reprezentująca pracownika wypożyczalni.
Sprzet.cs – klasa opisująca sprzęt dostępny w wypożyczalni.
KategoriaSprzetu.cs – klasa grupująca sprzęty według kategorii.
PozycjaWypozyczenia.cs – klasa opisująca pojedynczy sprzęt dodany do wypożyczenia.
Wypozyczenie.cs – główna klasa procesu wypożyczenia.
Platnosc.cs – klasa odpowiedzialna za płatności.
IPromocja.cs – interfejs promocji oraz przykładowe klasy rabatów.
DaneWypozyczalni.cs – główny kontener przechowujący wszystkie dane programu.
MenedzerPlikow.cs – klasa odpowiedzialna za zapis i odczyt danych z pliku JSON.
dane.json – plik z zapisanymi danymi programu.
Wypozyczalnia_sprzetow.csproj – plik konfiguracyjny projektu .NET.
Opis najważniejszych klas
Uzytkownik

Klasa abstrakcyjna, która jest bazą dla klas Klient i Pracownik.

Zawiera wspólne dane:

Id,
Imie,
Nazwisko,
Telefon.

Klasa posiada metodę abstrakcyjną PobierzOpis(), którą muszą zaimplementować klasy potomne. Dzięki temu w projekcie występuje polimorfizm.

Klient

Klasa reprezentuje klienta wypożyczalni. Dziedziczy po klasie Uzytkownik.

Oprócz danych podstawowych posiada numer dokumentu klienta. Klient jest przypisywany do wypożyczenia.

Pracownik

Klasa reprezentuje pracownika wypożyczalni. Również dziedziczy po klasie Uzytkownik.

Posiada dodatkowe pole Stanowisko, które określa funkcję pracownika, np. obsługa wypożyczalni lub serwis sprzętu.

Sprzet

Klasa opisuje pojedynczy sprzęt dostępny w wypożyczalni.

Przechowuje:

nazwę sprzętu,
typ sprzętu,
cenę za dobę,
rozmiar,
markę,
dostępność,
stan techniczny,
kategorię.

Klasa zawiera metody do:

sprawdzania dostępności,
oznaczania sprzętu jako wypożyczony,
oznaczania sprzętu jako dostępny,
zmiany stanu technicznego.
KategoriaSprzetu

Klasa służy do grupowania sprzętu według kategorii, np. narty, snowboard, akcesoria ochronne lub turystyka górska.

Kategoria może zawierać wiele sprzętów, dlatego występuje tutaj relacja agregacji.

PozycjaWypozyczenia

Klasa reprezentuje pojedynczy element wypożyczenia.

Jedno wypożyczenie może składać się z kilku pozycji, np. nart, kasku i kijków. Każda pozycja ma przypisany sprzęt oraz ilość.

Klasa posiada metodę ObliczKosztPozycji(), która liczy koszt danej pozycji na podstawie ceny za dobę, liczby dni i ilości.

Wypozyczenie

Najważniejsza klasa w projekcie. Reprezentuje cały proces wypożyczenia sprzętu.

Przechowuje:

klienta,
pracownika,
datę rozpoczęcia,
planowaną datę zwrotu,
rzeczywistą datę zwrotu,
status,
koszt całkowity,
listę pozycji wypożyczenia,
płatność.

Klasa zawiera metody:

DodajPozycje() – dodaje sprzęt do wypożyczenia,
Rozpocznij() – rozpoczyna wypożyczenie,
Zakoncz() – kończy wypożyczenie,
Anuluj() – anuluje wypożyczenie,
ObliczLiczbeDni() – oblicza czas wypożyczenia,
ObliczKoszt() – oblicza koszt całkowity,
SprawdzOpoznienie() – sprawdza, czy sprzęt oddano po terminie,
UtworzPlatnosc() – tworzy płatność do wypożyczenia.
Platnosc

Klasa odpowiada za płatność za wypożyczenie.

Przechowuje:

ID płatności,
ID wypożyczenia,
kwotę,
metodę płatności,
status płatności,
datę płatności.

Płatność może mieć status:

oczekująca,
zatwierdzona,
anulowana,
błąd.
IPromocja

Interfejs opisujący promocję. Każda promocja musi posiadać nazwę oraz metodę obliczającą rabat.

W projekcie znajdują się przykładowe promocje:

PromocjaWeekendowa,
PromocjaStalyKlient.

Interfejs pokazuje zastosowanie polimorfizmu, ponieważ różne promocje mają tę samą metodę ObliczRabat(), ale każda oblicza rabat inaczej.

DaneWypozyczalni

Klasa przechowuje wszystkie dane programu w jednym miejscu.

Zawiera listy:

klientów,
pracowników,
kategorii,
sprzętów,
wypożyczeń,
płatności.

Dzięki tej klasie cały stan programu można łatwo zapisać do pliku JSON.

MenedzerPlikow

Klasa statyczna odpowiedzialna za zapis i odczyt danych.

Zawiera metody:

ZapiszDoPliku() – zapisuje dane do pliku JSON,
WczytajZPliku() – odczytuje dane z pliku JSON,
NaprawRelacje() – odtwarza powiązania między obiektami po wczytaniu danych.

Metoda NaprawRelacje() jest potrzebna, ponieważ do pliku JSON zapisywane są głównie identyfikatory, np. KlientId, SprzetId, KategoriaId, a nie całe powiązane obiekty.

Zastosowane elementy programowania obiektowego
Dziedziczenie

Dziedziczenie występuje między klasą Uzytkownik a klasami Klient i Pracownik.

Dzięki temu wspólne pola, takie jak imię, nazwisko i telefon, są zapisane tylko raz.

Polimorfizm

Polimorfizm występuje w metodzie PobierzOpis(), która jest inaczej zaimplementowana w klasach Klient i Pracownik.

Polimorfizm występuje również przy interfejsie IPromocja, ponieważ różne klasy promocji mają tę samą metodę ObliczRabat(), ale działają według różnych zasad.

Kompozycja

Kompozycja występuje między Wypozyczenie a PozycjaWypozyczenia.

Wypożyczenie składa się z pozycji wypożyczenia, a każda pozycja oznacza konkretny sprzęt dodany do danego wypożyczenia.

Agregacja

Agregacja występuje między KategoriaSprzetu a Sprzet.

Kategoria grupuje sprzęty, ale sprzęt może istnieć jako osobny obiekt.

Agregacja występuje również w klasie DaneWypozyczalni, która przechowuje listy głównych obiektów programu.

Zapis i odczyt danych

Program zapisuje dane do pliku dane.json.

Do zapisu wykorzystywana jest serializacja JSON. Oznacza to, że obiekty C# są zamieniane na tekst w formacie JSON i zapisywane w pliku.

Do odczytu wykorzystywana jest deserializacja. Oznacza to, że tekst z pliku JSON jest zamieniany z powrotem na obiekty C#.

W projekcie zastosowano [JsonIgnore], aby nie zapisywać całych powiązanych obiektów i uniknąć zapętlenia danych. Zamiast tego zapisywane są identyfikatory, a po wczytaniu danych metoda NaprawRelacje() ponownie łączy obiekty.

Instrukcja użycia programu

Po uruchomieniu programu użytkownik wybiera opcje z menu, wpisując odpowiedni numer.

Przykładowe opcje programu:

1 – pokaż sprzęt,
2 – pokaż klientów,
3 – pokaż pracowników,
4 – pokaż wypożyczenia,
5 – pokaż płatności,
6 – dodaj sprzęt,
10 – wypożycz sprzęt,
11 – zwróć sprzęt,
0 – zakończ program.

Podczas tworzenia wypożyczenia użytkownik podaje ID sprzętu oraz liczbę dni wypożyczenia. Program sprawdza, czy sprzęt jest dostępny, tworzy wypożyczenie, oblicza koszt oraz tworzy płatność.

Podczas zwrotu sprzętu użytkownik podaje ID wypożyczenia. Program kończy wypożyczenie, ustawia rzeczywistą datę zwrotu i ponownie oznacza sprzęt jako dostępny.

Przykładowy przebieg działania
Program wczytuje dane z pliku dane.json.
Użytkownik wybiera opcję wyświetlenia sprzętu.
Program pokazuje listę dostępnych sprzętów.
Użytkownik wybiera opcję wypożyczenia.
Program pyta o ID sprzętu i liczbę dni.
Program sprawdza dostępność sprzętu.
Tworzone jest nowe wypożyczenie.
Sprzęt zostaje oznaczony jako niedostępny.
Program oblicza koszt wypożyczenia.
Tworzona jest płatność.
Dane mogą zostać zapisane do pliku JSON.
Przy kolejnym uruchomieniu programu dane zostaną ponownie wczytane.
Przykład danych w pliku JSON

Przykładowy sprzęt zapisany w pliku:

{
  "Id": 1,
  "Nazwa": "Narty Atomic Redster",
  "Typ": "Narty",
  "CenaZaDobe": 60,
  "Marka": "Atomic",
  "Dostepny": true,
  "StanTechniczny": "BardzoDobry",
  "KategoriaId": 1
}

Przykładowe wypożyczenie:

{
  "Id": 1,
  "KlientId": 1,
  "PracownikId": 1,
  "Status": "Aktywne",
  "KosztCalkowity": 240
}
Obsługa błędów

Program posiada podstawową obsługę błędów. Sprawdza między innymi:

czy podany sprzęt istnieje,
czy sprzęt jest dostępny,
czy sprzęt nie jest uszkodzony,
czy liczba dni jest większa od zera,
czy wypożyczenie można rozpocząć,
czy wypożyczenie można zakończyć,
czy płatność ma poprawną kwotę,
czy plik z danymi istnieje.

W przypadku błędu program wyświetla komunikat zamiast natychmiast się zamykać.

Uzasadnienie decyzji projektowych

Projekt został podzielony na wiele klas, aby każda część programu miała osobną odpowiedzialność. Dzięki temu kod jest bardziej czytelny i łatwiejszy do rozbudowy.

Klasa Wypozyczenie została zaprojektowana jako główna klasa procesu, ponieważ łączy klienta, pracownika, sprzęt, daty, koszt i płatność.

Klasa DaneWypozyczalni pełni rolę głównego kontenera danych, co ułatwia zapis i odczyt całego stanu programu.

Klasa MenedzerPlikow została oddzielona od reszty logiki, aby zapis i odczyt danych były w jednym miejscu.

Zastosowanie enumów pozwala ograniczyć błędy wynikające z wpisywania statusów lub typów jako zwykłego tekstu.

Podział pracy w zespole

Projekt został wykonany przez dwie osoby.

Podział pracy był równy, czyli 50% na 50%.

Obie osoby brały udział w:

tworzeniu klas,
omawianiu struktury programu,
testowaniu działania aplikacji,
poprawianiu błędów,
przygotowaniu końcowej wersji projektu.
Możliwe dalsze rozszerzenia projektu

Projekt można w przyszłości rozbudować o:

wybór pracownika z listy,
wybór metody płatności,
obsługę większej ilości sztuk danego sprzętu,
filtrowanie sprzętu po kategorii,
wyszukiwanie sprzętu po nazwie,
bardziej rozbudowany system promocji,
edycję danych klientów i pracowników,
Autorzy

Projekt wykonany zespołowo. Podział pracy: 50% / 50%.
