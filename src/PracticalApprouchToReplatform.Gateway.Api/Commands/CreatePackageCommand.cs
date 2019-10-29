namespace PracticalApprouchToReplatform.Gateway.Api.Commands
{
    public class CreatePackageCommand
    {
        public string Barcode { get; set; }
        public string Destination { get; set; }
        public string Source { get; set; }
    }
}