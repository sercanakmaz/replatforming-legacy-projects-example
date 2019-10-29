namespace PracticalApprouchToReplatform.New.Commands
{
    public class CreatePackageCommand: BaseCommand
    {
        public string Barcode { get; set; }
        public string Destination { get; set; }
    }
}