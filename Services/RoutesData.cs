using MapFollow.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapFollow.Services
{
    public class RoutesDataService
    {
        private readonly IMongoCollection<RouteData> _routeDatasCollection;
        private readonly IMongoCollection<Vehicule> _vehiculesCollection;


        public RoutesDataService(
            IOptions<RouteMapDatabase> routeDataStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                routeDataStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                routeDataStoreDatabaseSettings.Value.DatabaseName);

            _routeDatasCollection = mongoDatabase.GetCollection<RouteData>(
                routeDataStoreDatabaseSettings.Value.RoutesCollectionName);

            _vehiculesCollection = mongoDatabase.GetCollection<Vehicule>(
                routeDataStoreDatabaseSettings.Value.VehiculesCollectionName);
        }

        public async Task<List<RouteData>> GetAsync() =>
            await _routeDatasCollection.Find(_ => true).ToListAsync();

        public async Task<RouteData?> GetAsync(ObjectId id) =>
            await _routeDatasCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<RouteData>> GetVehiculeRouteDataAsync(int id) =>
            await _routeDatasCollection.Find(x => x.Vehicle_Id == id).ToListAsync();

        public async Task CreateAsync(RouteData newRouteData) =>
            await _routeDatasCollection.InsertOneAsync(newRouteData).ConfigureAwait(false);

        public async Task UpdateAsync(ObjectId id, RouteData updatedRouteData) =>
            await _routeDatasCollection.ReplaceOneAsync(x => x.Id == id, updatedRouteData);

        public async Task RemoveAsync(ObjectId id) =>
            await _routeDatasCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Vehicule>> GetAsyncVehicule() =>
            await _vehiculesCollection.Find(_ => true).ToListAsync();

        public async Task<Vehicule?> GetAsyncVehicule(string id) =>
            await _vehiculesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsyncVehicule(Vehicule newRouteData) =>
            await _vehiculesCollection.InsertOneAsync(newRouteData);

        public async Task UpdateAsyncVehicule(string id, Vehicule updatedRouteData) =>
            await _vehiculesCollection.ReplaceOneAsync(x => x.Id == id, updatedRouteData);

        public async Task RemoveAsyncVehicule(string id) =>
            await _vehiculesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
