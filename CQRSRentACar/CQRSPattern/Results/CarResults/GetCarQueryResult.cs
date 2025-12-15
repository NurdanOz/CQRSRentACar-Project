namespace CQRSRentACar.CQRSPattern.Results.CarResults
{
    public class GetCarQueryResult
    {
        public int CarId { get; set; }
        public string CarImage { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarSeat { get; set; }
        public string CarTransmission { get; set; }
        public string CarFuel { get; set; }
        public string CarYear { get; set; }
        public string CarKm { get; set; }
        public decimal CarReview { get; set; }
        public decimal CarPrice { get; set; }
    }
}
