using System;

namespace WypozyczalniaSprzetu
{
    public class Sprzet
    {
        public string Nazwa { get; set; }
        public string Typ { get; set; }
        public string Rozmiar { get; set; }
        public string Marka { get; set; }
        public string StanTechniczny { get; set; }
        public bool Dostepny { get; set; }

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
        public void ZmienStanTechniczny(string nowyStan)
        {
            StanTechniczny = nowyStan;
        }
    }
}