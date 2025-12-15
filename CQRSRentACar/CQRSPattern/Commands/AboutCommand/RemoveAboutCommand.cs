namespace CQRSRentACar.CQRSPattern.Commands.AboutCommand
{
    public class RemoveAboutCommand
    {
        public int AboutId { get; set; }

        public RemoveAboutCommand(int aboutId)
        {
            AboutId = aboutId;
        }
    }
}
