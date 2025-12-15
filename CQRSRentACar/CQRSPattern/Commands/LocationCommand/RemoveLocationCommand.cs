namespace CQRSRentACar.CQRSPattern.Commands.LocationCommand
{
    public class RemoveLocationCommand
    {
        public int LocationId { get; set; }

        public RemoveLocationCommand(int locationId)
        {
            LocationId = locationId;
        }
    }
}
