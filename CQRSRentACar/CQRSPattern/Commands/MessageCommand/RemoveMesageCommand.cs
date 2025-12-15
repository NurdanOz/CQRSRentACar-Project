namespace CQRSRentACar.CQRSPattern.Commands.MessageCommand
{
    public class RemoveMesageCommand
    {
        public int MessageId { get; set; }

        public RemoveMesageCommand(int messageId)
        {
            MessageId = messageId;
        }
    }
}
