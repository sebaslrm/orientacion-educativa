using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BaseDatos;

public abstract class Repository
{
    public Repository(IMongoClient client, IOptions<MongoDbSettings> options)
    {
    }
}