namespace CQRSRentACar.CQRSPattern.Commands.CarCommand
{
    public class RemoveCarCommand
    {
        public int CarId { get; set; }

        public RemoveCarCommand(int carId)
        {
            CarId = carId;
        }
    }
}
