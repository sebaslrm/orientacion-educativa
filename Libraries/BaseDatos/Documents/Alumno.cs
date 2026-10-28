using MongoDB.Bson;

namespace BaseDatos.Documents;

public class Alumno
{
    public ObjectId Id { get; set; }
    public long NumControl { get; set; }
    public string Paterno { get; set; } = string.Empty;
    public string Materno { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int Generacion { get; set; }
    public string Carrera { get; set; } = string.Empty;
    public int Semestre { get; set; }
    public string Grupo { get; set; } = string.Empty;
}