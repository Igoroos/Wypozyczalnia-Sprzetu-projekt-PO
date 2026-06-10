# Wypozyczalnia-Sprzetu-projekt-PO
# Opis projektu – Wypożyczalnia sprzętu górskiego

## Cel projektu

Celem projektu jest stworzenie programu do obsługi wypożyczalni sprzętu górskiego. Program pomaga zarządzać sprzętem, klientami, pracownikami, wypożyczeniami oraz płatnościami. Rozwiązuje problem ręcznego zapisywania wypożyczeń i obliczania kosztów.

Program jest skierowany do małych wypożyczalni sprzętu sportowego lub górskiego oraz jako projekt edukacyjny pokazujący programowanie obiektowe w języku C#.

Główne funkcjonalności programu:

* przechowywanie danych klientów, pracowników i sprzętu,
* tworzenie wypożyczeń,
* dodawanie sprzętu do wypożyczenia,
* sprawdzanie dostępności sprzętu,
* naliczanie kosztu wypożyczenia,
* obsługa płatności,
* zapis i odczyt danych z pliku JSON.

## Architektura i struktura obiektowa

Projekt został zbudowany w oparciu o programowanie obiektowe. Oznacza to, że najważniejsze elementy wypożyczalni zostały przedstawione jako osobne klasy. Każda klasa ma własną odpowiedzialność i opisuje jeden konkretny element systemu, na przykład klienta, pracownika, sprzęt, wypożyczenie albo płatność.

Taki podział sprawia, że program jest bardziej czytelny i łatwiejszy do rozbudowy. Zamiast umieszczać całą logikę w jednym pliku, projekt został podzielony na wiele mniejszych części. Dzięki temu łatwiej znaleźć odpowiedni fragment kodu, poprawić błąd lub dodać nową funkcjonalność.

Główna logika programu opiera się na klasie `Wypozyczenie`, ponieważ to ona łączy najważniejsze elementy: klienta, pracownika, wypożyczany sprzęt, daty, koszt oraz płatność. Dane programu są przechowywane w klasie `DaneWypozyczalni`, natomiast za zapis i odczyt danych z pliku odpowiada klasa `MenedzerPlikow`.

Najważniejsze klasy:

* `Uzytkownik` – klasa bazowa dla użytkowników systemu. Zawiera wspólne dane, takie jak imię, nazwisko i telefon.
* `Klient` – dziedziczy po `Uzytkownik`. Reprezentuje osobę wypożyczającą sprzęt.
* `Pracownik` – dziedziczy po `Uzytkownik`. Reprezentuje pracownika obsługującego wypożyczenie.
* `Sprzet` – opisuje sprzęt dostępny w wypożyczalni, jego cenę, kategorię, stan techniczny i dostępność.
* `KategoriaSprzetu` – grupuje sprzęty według kategorii, np. narty, kaski lub snowboard.
* `PozycjaWypozyczenia` – oznacza pojedynczy sprzęt dodany do wypożyczenia.
* `Wypozyczenie` – główna klasa procesu wypożyczenia. Łączy klienta, pracownika, sprzęt, daty, status i koszt.
* `Platnosc` – odpowiada za płatność za wypożyczenie.
* `IPromocja` – interfejs pozwalający tworzyć różne promocje i rabaty.
* `DaneWypozyczalni` – przechowuje wszystkie listy danych programu.
* `MenedzerPlikow` – odpowiada za zapis i odczyt danych z pliku JSON oraz odtwarzanie relacji między obiektami po wczytaniu danych.

## Relacje między klasami

W projekcie występuje **dziedziczenie**, ponieważ `Klient` i `Pracownik` dziedziczą po klasie `Uzytkownik`. Dzięki temu wspólne pola, takie jak imię, nazwisko i telefon, nie muszą być powtarzane w obu klasach.

Występuje też **polimorfizm**, ponieważ klasy `Klient` i `Pracownik` inaczej implementują metodę `PobierzOpis()`. Oznacza to, że ten sam typ bazowy `Uzytkownik` może zachowywać się różnie w zależności od tego, czy obiekt jest klientem, czy pracownikiem. Polimorfizm pojawia się również przy interfejsie `IPromocja`, ponieważ różne promocje mogą mieć własny sposób obliczania rabatu.

Między `Wypozyczenie` a `PozycjaWypozyczenia` występuje **kompozycja**, ponieważ wypożyczenie składa się z pozycji wypożyczenia. Każda pozycja oznacza konkretny sprzęt dodany do danego wypożyczenia.

Między `KategoriaSprzetu` a `Sprzet` występuje **agregacja**, ponieważ kategoria grupuje sprzęty, ale sprzęt może istnieć jako osobny obiekt. Na przykład kategoria „narty” może zawierać kilka różnych sprzętów, ale każdy sprzęt posiada własne dane, takie jak nazwa, cena, marka i stan techniczny.

`DaneWypozyczalni` również pełni rolę klasy agregującej dane, ponieważ przechowuje listy klientów, pracowników, sprzętów, kategorii, wypożyczeń i płatności. Dzięki temu cały stan programu znajduje się w jednym obiekcie, który można łatwo zapisać do pliku i później odczytać.

Między `Wypozyczenie` a `Klient`, `Pracownik` i `Platnosc` występują powiązania, ponieważ każde wypożyczenie ma przypisanego klienta, może mieć pracownika obsługującego oraz płatność. Dodatkowo `PozycjaWypozyczenia` jest powiązana ze sprzętem, ponieważ wskazuje, jaki konkretnie sprzęt został wypożyczony.

## Uzasadnienie decyzji projektowych

Podział na klasy ułatwia zrozumienie programu i pozwala każdej klasie odpowiadać za jedną konkretną część systemu. Klasa `Wypozyczenie` została zaprojektowana jako główna klasa procesu, ponieważ łączy klienta, pracownika, sprzęt, koszt i płatność.

Zastosowanie dziedziczenia pozwala uniknąć powtarzania wspólnych pól w klasach `Klient` i `Pracownik`. Interfejs `IPromocja` umożliwia łatwe dodawanie nowych rodzajów rabatów. Klasa `MenedzerPlikow` oddziela zapis i odczyt z pliku od reszty logiki programu, dzięki czemu kod jest bardziej uporządkowany.

## Podział pracy w zespole

Praca w zespole została podzielona po równo, czyli 50% na 50%. Obie osoby brały udział w tworzeniu klas, omawianiu struktury programu, testowaniu działania oraz poprawianiu błędów. Dzięki równemu podziałowi pracy każda osoba miała podobny wkład w wykonanie projektu.