using System;
using System.ComponentModel.DataAnnotations;

namespace grud_backend.Models
{
    public class Product
    {

        public string Id { get; set; }
        public string IdProveedor { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public float CostoUnitario { get; set; }
        public int Stock { get; set; }
        public float CostoTotal { get; set; }
        public bool Estatus { get; set; }
        public float PrecioClienteU { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public float Ganancia { get; set; }

    }
}