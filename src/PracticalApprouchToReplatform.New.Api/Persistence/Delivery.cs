using System;
using MongoDB.Bson;

namespace PracticalApprouchToReplatform.New.Api.Persistence
{
    public class Delivery
    {
        public Delivery()
        {
            
        }

        public Delivery(string barcode, string destination)
        {
            this.Barcode = barcode;
            this.Destination = destination;
        }
        public ObjectId Id { get; set; }
        public string Barcode { get; set; }
        public string Destination { get; set; }
    }
}