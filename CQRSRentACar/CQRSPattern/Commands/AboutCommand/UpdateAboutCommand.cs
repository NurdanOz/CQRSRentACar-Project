namespace CQRSRentACar.CQRSPattern.Commands.AboutCommand
{
    public class UpdateAboutCommand
    {
        public int AboutId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
    }
}
