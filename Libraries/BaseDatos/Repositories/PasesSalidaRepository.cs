using System.Collections.Generic;
using System.Threading.Tasks;
using BaseDatos.Documents;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BaseDatos.Repositories
{
    public class PasesSalidaRepository : Repository
    {
        /*
        a)Insertar un pase de salida.
        b)Modificar un pase de salida.
        c)Eliminar un pase de salida.
        d)Consultar un pase de salida 
         e)Consultar todos los pases de salida.
          */
        private readonly IMongoCollection<PaseSalida> pases;

        public PasesSalidaRepository(IMongoClient client, IOptions<MongoDbSettings> options) : base(client, options)
        {
            this.pases = client.GetDatabase(options.Value.DatabaseName)
                .GetCollection<PaseSalida>("pases de salida");
        }

        public async Task InsertarPase(PaseSalida item)
        {
            await this.pases.InsertOneAsync(item);
        }

        public async Task ModificarPase(PaseSalida item)
        {
            await this.pases.UpdateOneAsync(
                x => x.Id == item.Id,
                Builders<PaseSalida>.Update
                .Set(x => x.Fecha, item.Fecha)
                .Set(x => x.Motivo, item.Motivo)
                .Set(x => x.NombreTutor, item.NombreTutor)
                .Set(x => x.Parentesco, item.Parentesco)
                .Set(x => x.Telefono, item.Telefono)
                .Set(x => x.Comentarios, item.Comentarios)
                );
        }

        public async Task EliminarPase(ObjectId id)
        {
            await this.pases.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<PaseSalida> Consultar(ObjectId id)
        {
            var respuesta = await this.pases.FindAsync(x => x.Id == id);
            var pase = await respuesta.FirstOrDefaultAsync();
            return pase;
        }

        public async Task<List<PaseSalida>> ListarPasesAlumno(long numControl)
        {
            var respuesta = await this.pases.FindAsync(x => x.NumControl == numControl);
            var lista = await respuesta.ToListAsync();
            return lista;
        }
    }
}