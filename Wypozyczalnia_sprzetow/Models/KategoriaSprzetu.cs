using System.Text.Json.Serialization;

namespace WypozyczalniaSprzetuGorskiego.Models
{
    public class KategoriaSprzetu
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;

        [JsonIgnore]
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
                throw new ArgumentNullException(nameof(sprzet));
            }

            if (!Sprzety.Any(s => s.Id == sprzet.Id))
            {
                Sprzety.Add(sprzet);
                sprzet.KategoriaId = Id;
                sprzet.Kategoria = this;
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
            sprzet.KategoriaId = 0;
            sprzet.Kategoria = null;
            return true;
        }

        public override string ToString()
        {
            return $"{Id}. {Nazwa} - {Opis}";
        }
    }
}
