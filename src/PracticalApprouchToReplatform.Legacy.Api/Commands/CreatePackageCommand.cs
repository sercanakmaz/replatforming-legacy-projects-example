namespace PracticalApprouchToReplatform.Legacy.Api.Commands
{
    public class CreatePackageCommand: BaseCommand
    {
        public string Barcode { get; set; }
        public string Destination { get; set; }
    }
}