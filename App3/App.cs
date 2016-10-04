using Android.App;
using Android.Runtime;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System;

namespace AudioTour
{
    [Application]
    public class App : Application
    {
        public App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            string uri = "mongodb://coala:1audio2@ds049496.mlab.com:49496/audiotour";
            var client = new MongoClient(uri);
            var db = client.GetDatabase("audiotour");

            BsonDocument teste = new BsonDocument
            {
                {"userName", "Rafael"},
                {"userEmail", "asdf@asdf.com"}
            };

            var users = db.GetCollection<BsonDocument>("users");

            users.InsertOne(teste);
        }
    }
}