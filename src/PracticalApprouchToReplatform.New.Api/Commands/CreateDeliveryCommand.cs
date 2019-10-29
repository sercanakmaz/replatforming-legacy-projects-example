namespace PracticalApprouchToReplatform.New.Api.Commands
{
    public class CreateDeliveryCommand: BaseCommand
    {
        public string Barcode { get; set; }
        public string Destination { get; set; }
    }
}