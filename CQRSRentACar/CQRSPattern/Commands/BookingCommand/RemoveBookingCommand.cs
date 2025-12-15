namespace CQRSRentACar.CQRSPattern.Commands.BookingCommand
{
    public class RemoveBookingCommand
    {
        public int BookingId { get; set; }

        public RemoveBookingCommand(int bookingId)
        {
            BookingId = bookingId;
        }
    }
}
