using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.ControlLib.Driver
{
    public class SyncController
    {
        private IMongoClient _clients = null;

        private IMongoDatabase _dataBase = null;

        private IMongoCollection<BsonDocument> _currentCollection = null;

        public IMongoClient Connect(string connectionString)
        {
            //MongoClientSettings settings = new MongoClientSettings();
            //settings.ConnectionMode = ConnectionMode.Automatic;
            _clients = new MongoClient(connectionString);

            return _clients;
        }

        public IMongoDatabase GetDataBase(IMongoClient client, string DBName)
        {
            _dataBase = _clients.GetDatabase(DBName);

            return _dataBase;
        }

        public IMongoCollection<BsonDocument> CreateCollection(string collectionName)
        {
            _dataBase.CreateCollection(collectionName);

            _currentCollection = _dataBase.GetCollection<BsonDocument>(collectionName);

            return _currentCollection;
        }

        public IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            _currentCollection = _dataBase.GetCollection<BsonDocument>(collectionName);

            return _currentCollection;
        }

        public void SelectCollection(string collectionName)
        {
            _currentCollection = _dataBase.GetCollection<BsonDocument>(collectionName);
        }

        public void Insert(string collectionName, string JSONDocument)
        {
            if (_currentCollection == null)
            {
                SelectCollection(collectionName);
            }

            try
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(JSONDocument);

                BsonDocument document = obj.ToBsonDocument(obj.GetType());

                _currentCollection.InsertOne(document);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReplaceDocument(string JSONDocument)
        {

        }

        public void UpdateDocument()
        {

        }
    }
}
