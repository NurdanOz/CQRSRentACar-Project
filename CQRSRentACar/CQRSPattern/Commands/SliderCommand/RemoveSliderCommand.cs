namespace CQRSRentACar.CQRSPattern.Commands.SliderCommand
{
    public class RemoveSliderCommand
    {
        public int SliderId { get; set; }

        public RemoveSliderCommand(int sliderId)
        {
            SliderId = sliderId;
        }
    }
}
