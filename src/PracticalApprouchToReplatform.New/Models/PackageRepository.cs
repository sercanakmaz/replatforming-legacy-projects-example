using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace PracticalApprouchToReplatform.New.Models
{
    public interface IPackageRepository
    {
        Task Add(Package package);
    }
    public class PackageRepository:IPackageRepository
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<Package> _collection;

        public PackageRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _collection = this._mongoDatabase.GetCollection<Package>(nameof(Package));
        }

        public async Task Add(Package package)
        {
            await _collection.InsertOneAsync(package);
        }
    }
}