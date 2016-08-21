using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.ControlLib.Driver;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MongoDB.ControlLib.Driver.Test
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void CreateCollectionFactory()
        {
            CollectionFactory cf = new CollectionFactory();
            IPAddress ip = IPAddress.Parse("192.168.1.141");
            cf.Connect(ip);

            MongoCollection mc = cf.GetCollection("test");

            string data = JsonConvert.SerializeObject(new { name = "admin", password = "admin", role = new int[] { 1, 2, 3, 4, 5 } });

            mc.Insert(data);

        }
        public void InsertMany()
        {
            CollectionFactory cf = new CollectionFactory();
            IPAddress ip = IPAddress.Parse("192.168.1.141");
            cf.Connect(ip);

            MongoCollection mc = cf.GetCollection("test");

            for (int i = 0; i < 1000; i++)
            {
                string data = JsonConvert.SerializeObject(new { name = "admin" + i, password = "admin", role = new int[] { 1, 2, 3, 4, 5 } });

                mc.Insert(data);
            }
        }
        public void Update()
        {
            CollectionFactory cf = new CollectionFactory();
            IPAddress ip = IPAddress.Parse("192.168.1.141");
            cf.Connect(ip);

            MongoCollection mc = cf.GetCollection("test");
            UpdateParameter up = new UpdateParameter();
            up.Add(new KeyValuePair<object, object>("password", "123456"));

            Filter filter = new Filter();

            filter.Add(new KeyValuePair<object, object>("name", "admin11"));
            mc.UpdateDocument(filter, up);
        }
        public void UpdateMany()
        {
            CollectionFactory cf = new CollectionFactory();
            IPAddress ip = IPAddress.Parse("192.168.1.141");
            cf.Connect(ip);

            MongoCollection mc = cf.GetCollection("test");
            UpdateParameter up = new UpdateParameter();
            up.Add(new KeyValuePair<object, object>("password", "admin"));

            Filter filter = new Filter();

            filter.Add(new KeyValuePair<object, object>("password", "administrator"));
            mc.UpdateDocument(filter, up);
        }
        public void Delete()
        {
            CollectionFactory cf = new CollectionFactory();
            IPAddress ip = IPAddress.Parse("192.168.1.141");
            cf.Connect(ip);

            MongoCollection mc = cf.GetCollection("test");

            Filter filter = new Filter();

            filter.Add(new KeyValuePair<object, object>("password", "administrator"));

            mc.DeleteDocument(filter);
        }

    }
}
