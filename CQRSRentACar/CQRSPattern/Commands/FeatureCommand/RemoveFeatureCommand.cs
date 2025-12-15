namespace CQRSRentACar.CQRSPattern.Commands.FeatureCommand
{
    public class RemoveFeatureCommand
    {
        public int FeatureId { get; set; }

        public RemoveFeatureCommand(int featureId)
        {
            FeatureId = featureId;
        }
    }
}
