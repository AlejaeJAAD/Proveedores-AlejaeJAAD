using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace grud_backend.Models
{
    public class User
    {
        public string Id { get; set;}
        public string Rol {get; set;}
        public string Imagen {get; set;}
        [Required]
        public string Username{get;set;}
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get ; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Bio { get; set; }
        public string Logo { get; set; }
        public string NombreEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string RFC { get; set; }
        public string SitioWeb { get; set; }
        public DateTime FechaNacimiento {get; set;}
        public string Token { get; set; }
        public int Cp { get; set; }
        public int Numero { get; set; }
        [Required]
        public bool Estatus {get; set;}
    }

    public class Estatus
    {
        public bool estatus { get; set; }
    }

    public class ProvCliente
    {

        public string Id { get; set; }
        public string IdProveedor { get; set; }
        public string IdCliente { get; set; }
        public DateTime FechaAfiliacion { get; set; }
        public bool Aceptado { get; set; }

    }

}
