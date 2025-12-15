
namespace CQRSRentACar.Models
{
    public class DistanceCalculationResult
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public double DistanceKm { get; set; }
        public int EstimatedDurationMinutes { get; set; }
        public string FuelType { get; set; }
        public decimal CurrentFuelPrice { get; set; }
        public double AverageFuelConsumption { get; set; }
        public decimal FuelCost { get; set; }
        public decimal ExtraFee { get; set; }
        public decimal TotalCost { get; set; }
    }

    
    public class RapidApiDistanceResponse
    {
        public DistanceRequest Request { get; set; }
        public DistanceResponse Response { get; set; }
    }

    public class DistanceRequest
    {
        public string From_City { get; set; }
        public string To_City { get; set; }
    }

    public class DistanceResponse
    {
        public Geodesic Geodesic { get; set; }
        public Greatcircle Greatcircle { get; set; }
    }

    public class Geodesic
    {
        public string Unit { get; set; }
        public double Value { get; set; }
    }

    public class Greatcircle
    {
        public string Unit { get; set; }
        public double Value { get; set; }
    }
}