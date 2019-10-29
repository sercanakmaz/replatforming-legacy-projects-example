using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace PracticalApprouchToReplatform.New.Api.Persistence
{
    public interface IDeliveryRepository
    {
        Task Add(Delivery delivery);
    }

    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly IMongoCollection<Delivery> _collection;

        public DeliveryRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<Delivery>(nameof(Delivery));

            _collection.Indexes.CreateOne(new CreateIndexModel<Delivery>(
                Builders<Delivery>.IndexKeys.Ascending(_ => _.Barcode), new CreateIndexOptions {Unique = true}));
        }

        public async Task Add(Delivery delivery)
        {
            await _collection.InsertOneAsync(delivery);
        }
    }
}