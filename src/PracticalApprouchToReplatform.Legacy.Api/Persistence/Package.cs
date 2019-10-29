namespace PracticalApprouchToReplatform.Legacy.Api.Persistence
{
    public class Package
    {
        public Package()
        {
            
        }

        public Package(string barcode, string destination)
        {
            this.Barcode = barcode;
            this.Destination = destination;
        }
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Destination { get; set; }
    }
}