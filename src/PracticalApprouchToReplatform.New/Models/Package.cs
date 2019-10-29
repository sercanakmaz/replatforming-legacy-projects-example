using System;
using MongoDB.Bson;

namespace PracticalApprouchToReplatform.New.Models
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
        public ObjectId Id { get; set; }
        public string Barcode { get; set; }
        public string Destination { get; set; }
    }
}