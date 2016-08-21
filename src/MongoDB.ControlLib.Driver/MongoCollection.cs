using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDB.ControlLib.Driver
{
    public class MongoCollection
    {
        /// <summary>
        /// 集合名称
        /// </summary>
        public string Name { get; }

        internal IMongoCollection<BsonDocument> Collection { get; set; }

        internal MongoCollection(string name)
        {
            Name = name;
        }

        public void Insert(string JSONDocument)
        {
            try
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(JSONDocument);
                BsonDocument document = obj.ToBsonDocument(obj.GetType());
                Collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除对应的ID 文档
        /// </summary>
        /// <param name="id">文档列表</param>
        public void DeleteDocument(params string[] id)
        {
            bool hasException = false;
            List<string> errorid = new List<string>();
            Exception msg = null;
            foreach (var item in id)
            {
                try
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", item);
                    Collection.DeleteOne(filter);
                }
                catch (Exception ex)
                {
                    hasException = true;
                    errorid.Add(item);
                    msg = ex;
                    continue;
                }
            }
            if (hasException)
            {
                string errorMessage = string.Format("Delete Error In {0}", string.Join(",", errorid));
                Exception ex = new Exception(errorMessage, msg);
                throw ex;
            }
        }
        public void DeleteDocument(Filter filter)
        {

            var docFilter = Builders<BsonDocument>.Filter.Eq(filter[0].Key.ToString(), filter[0].Value.ToString());

            Collection.DeleteMany(docFilter);
        }


        public void UpdateDocument(Filter filters, UpdateParameter param)
        {
            var filterbuilder = Builders<BsonDocument>.Filter;
            var updateFilter = filterbuilder.Eq("1", "1");
            foreach (var item in filters)
            {
                updateFilter = updateFilter & filterbuilder.Eq(item.Key.ToString(), item.Value);

            }

            var updateBuilder = Builders<BsonDocument>.Update;
            UpdateDefinition<BsonDocument> updateSet = null;

            foreach (var item in param)
            {
                updateSet = updateBuilder.AddToSet(item.Key.ToString(), item.Value);
            }

            Collection.UpdateMany(updateFilter, updateSet);
        }


        public List<T> List<T>() where T : new()
        {
            List<T> list = new List<T>();

            T t = new T();





            return list;
        }
    }
}
