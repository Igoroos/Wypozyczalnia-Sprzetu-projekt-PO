using System;
using System.Collections.Generic;

namespace WypozyczalniaSprzetu
{
    public class KategoriaSprzetu
    {
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public List<Sprzet> Sprzety { get; set; } = new List<Sprzet>();
    }
}