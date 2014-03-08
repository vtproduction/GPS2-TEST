using System.Configuration;
using MongoDB.Driver;

namespace MongoMembership.Helpers
{
    public class MongoHelper<T> where T : class
    {
        public MongoCollection<T> Collection { get; private set; }

        public MongoHelper()
        {
            //var con = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MongoUri"].ConnectionString);

            //var server = MongoServer.Create("server=127.0.0.1;database=GPS2");
            //var db = server.GetDatabase("GPS2");
            //Collection = db.GetCollection<T>(typeof(T).Name);

            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("GPS2");
            Collection = database.GetCollection<T>(typeof(T).Name);

        }
    }
}