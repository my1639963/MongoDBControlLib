using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;

namespace MongoDB.ControlLib.Driver
{
    public class CollectionFactory
    {
        private IMongoClient _client = null;

        private IMongoDatabase _database = null;

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="ipaddress">MongoDB服务器IPv4地址</param>
        /// <param name="port">MongoDB服务器端口号，默认27017</param>
        /// <param name="userName">登陆服务器用户名，默认admin</param>
        /// <param name="password">登陆服务器密码，默认admin</param>
        /// <param name="dbName">需要链接的数据库名称，默认admin</param>
        /// <returns></returns>
        public void Connect(IPAddress ipaddress, ushort port = 27017, string userName = "admin", string password = "admin", string dbName = "admin")
        {
            //MongoClientSettings settings = new MongoClientSettings();
            //settings.ConnectionMode = ConnectionMode.Automatic;
            try
            {
                if (_client == null)
                {
                    string connectionString = string.Format("mongodb://{0}:{1}@{2}:{3}", userName, password, ipaddress.ToString(), port);
                    _client = new MongoClient(connectionString);
                    _database = _client.GetDatabase(dbName);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 返回指定的一个MongoCollection实例。通过实例完成基本操作
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public MongoCollection GetCollection(string collectionName)
        {
            //_database.CreateCollection(collectionName);
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            MongoCollection mc = new MongoCollection(collectionName);
            mc.Collection = collection;

            return mc;
        }
    }
}
