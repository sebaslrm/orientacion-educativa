using BaseDatos.Documents;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BaseDatos.Repositories;

public class AlumnosRepository : Repository
{
    private readonly IMongoCollection<Alumno> alumnos;
    public AlumnosRepository(IMongoClient client, IOptions<MongoDbSettings> options) : base(client, options)
    {
        var db = client.GetDatabase(options.Value.DatabaseName);
        this.alumnos = db.GetCollection<Alumno>("alumnos");
    }

    public async Task<List<Alumno>> ListarTodos()
    {
        var alumnos = await this.alumnos.FindAsync(x => true);
        return alumnos.ToList();
    }

    public async Task<Alumno?> ObtenerAlumnoPorNumeroDeControl(long numControl)
    {
        var respuesta = await this.alumnos.FindAsync(x => x.NumControl == numControl);
        var alumno = await respuesta.FirstOrDefaultAsync();

        return alumno;
    }
}