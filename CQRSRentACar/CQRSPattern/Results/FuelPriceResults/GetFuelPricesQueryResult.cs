using CQRSRentACar.Models;

namespace CQRSRentACar.CQRSPattern.Results.FuelPriceResults
{
    public class GetFuelPricesQueryResult
    {
        
        public List<FuelPrice> FuelPrices { get; set; }
    }
}
