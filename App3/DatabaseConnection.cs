using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Threading.Tasks;

/*
 * This class manages all Database related functions.  
 */
namespace AudioTour
{
    class DatabaseConnection
    {
        //Reference to dataBase
        private static IMongoDatabase dataBase;

        /*
         * Gets the reference to the database
         */
        public DatabaseConnection()
        {
            
            string uri = "mongodb://coala:1audio2@ds049496.mlab.com:49496/audiotour";
            var client = new MongoClient(uri);
            dataBase = client.GetDatabase("audiotour");
        }

        /*
         * Add new user
         */
        public async Task addNewUser(string name, string email)
        {
            BsonDocument newUser = new BsonDocument
            {
                {"name", name},
                {"email", email}
            };

            var users = dataBase.GetCollection<BsonDocument>("users");

            await users.InsertOneAsync(newUser);
        }
    }
}