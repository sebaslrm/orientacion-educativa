using System;
using MongoDB.Bson;

namespace BaseDatos.Documents
{
    public class PaseSalida
    {
        public ObjectId Id { get; set; }
        public long NumControl { get; set; }
        public string Carrera { get; set; } = string.Empty;
        public int Semestre { get; set; }
        public string Grupo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string NombreTutor { get; set; } = string.Empty;
        public string Parentesco { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Comentarios { get; set; } = string.Empty;
    }
}