using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BaseDatos.Documents;

[BsonIgnoreExtraElements]
public class Alumno
{
    public ObjectId Id { get; set; }
    public long NumControl { get; set; }

    [BsonElement("apellidoPaterno")]
    public string Paterno { get; set; } = string.Empty;

    [BsonElement("apellidoMaterno")]
    public string Materno { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;
    public int Generacion { get; set; }
    public string Carrera { get; set; } = string.Empty;
    public int Semestre { get; set; }
    public string Grupo { get; set; } = string.Empty;
    public string? Foto { get; set; }
}