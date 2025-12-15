// CQRSPattern/Results/DistanceResults/GetDistanceQueryResult.cs
using CQRSRentACar.Models;

namespace CQRSRentACar.CQRSPattern.Results.DistanceResults
{
    public class GetDistanceQueryResult
    {
        public bool Success { get; set; }
        public DistanceCalculationResult Data { get; set; }
        public string Message { get; set; }
    }
}