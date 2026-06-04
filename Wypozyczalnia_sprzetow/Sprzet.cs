using System;

namespace WypozyczalniaSprzetu
{
    public enum StanSprzetu
    {
        Idealny,
        Dobry,
        Uszkodzony,
        WymagaNaprawy
    }

    public class Sprzet
    {
        public string Nazwa { get; set; }
        public string Typ { get; set; }
        public string Rozmiar { get; set; }
        public string Marka { get; set; }
        
        public StanSprzetu StanTechniczny { get; set; } 
        
        public bool Dostepny { get; set; }
        public decimal CenaBazowa { get; set; }

        public bool SprawdzDostepnosc()
        {
            return Dostepny;
        }

        public void OznaczJakoWypozyczony()
        {
            Dostepny = false;
        }

        public void OznaczJakoDostepny()
        {
            Dostepny = true;
        }

        public void ZmienStanTechniczny(StanSprzetu nowyStan)
        {
            StanTechniczny = nowyStan;
        }
    }
}