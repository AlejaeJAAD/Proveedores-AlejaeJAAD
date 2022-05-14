﻿using System;
using System.Collections.Generic;

namespace grud_backend.Models
{
    public class Afiliacion
    {
        public string RecipientId { get; set; }
        public DateTime FechaAfiliacion { get; set; }
        public bool Aceptado { get; set; }
    }
    public class AfiliacionRequest
    {
        public AfiliacionRequest(string Id, string SenderId, List<Afiliacion> MyAfiliates)
        {
            this.Id = Id;
            this.SenderId = SenderId;
            this.MyAfiliates = MyAfiliates;
        }
        public string Id { get; set; }
        public string SenderId { get; set; }
        public List<Afiliacion> MyAfiliates { get; set; }
    }
    public class AfiliacionResponse
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public List<User> MyAfiliates { get; set; }
    }

    public class Notificaciones
    {
        public string Id { get; set; }
        public List<Notificacion> LNotificaciones { get; set; }
        
    }
    public class Notificacion
    {
        public string Id { get; set; }
        public int Tipo { get; set; }
    }

}
