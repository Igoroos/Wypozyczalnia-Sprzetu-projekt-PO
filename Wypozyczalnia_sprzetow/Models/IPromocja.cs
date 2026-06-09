namespace WypozyczalniaSprzetuGorskiego.Models
{
    public interface IPromocja
    {
        string NazwaPromocji { get; }
        decimal ObliczRabat(decimal kwotaBazowa);
    }

    public class PromocjaWeekendowa : IPromocja
    {
        public string NazwaPromocji => "Zniżka Weekendowa (-10%)";
        public decimal ObliczRabat(decimal kwotaBazowa)
        {
            return kwotaBazowa * 0.10m;
        }
    }

    public class PromocjaStalyKlient : IPromocja
    {
        public string NazwaPromocji => "Zniżka Stałego Klienta (-50 zł przy kwocie > 100 zł)";
        public decimal ObliczRabat(decimal kwotaBazowa)
        {
            if (kwotaBazowa > 100m) return 50m;
            return 0m;
        }
    }
}