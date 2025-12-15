namespace CQRSRentACar.CQRSPattern.Commands.ServiceCommand
{
    public class RemoveServiceCommand
    {
        public int ServiceId { get; set; }

        public RemoveServiceCommand(int serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
