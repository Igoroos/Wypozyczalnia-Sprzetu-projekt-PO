namespace WypozyczalniaSprzetuGorskiego.Models
{
    public class KategoriaSprzetu
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public List<Sprzet> Sprzety { get; set; } = new List<Sprzet>();

        public KategoriaSprzetu()
        {
        }

        public KategoriaSprzetu(int id, string nazwa, string opis)
        {
            Id = id;
            Nazwa = nazwa;
            Opis = opis;
        }

        public void DodajSprzet(Sprzet sprzet)
        {
            if (sprzet == null)
            {
                throw new ArgumentNullException(nameof(sprzet), "Nie można dodać pustego sprzętu.");
            }

            if (!Sprzety.Contains(sprzet))
            {
                Sprzety.Add(sprzet);
                sprzet.Kategoria = this;
                sprzet.KategoriaId = Id;
            }
        }

        public bool UsunSprzet(int idSprzetu)
        {
            Sprzet? sprzet = Sprzety.FirstOrDefault(s => s.Id == idSprzetu);

            if (sprzet == null)
            {
                return false;
            }

            Sprzety.Remove(sprzet);
            sprzet.Kategoria = null;
            sprzet.KategoriaId = 0;
            return true;
        }

        public override string ToString()
        {
            return $"{Id}. {Nazwa} - {Opis}";
        }
    }
}