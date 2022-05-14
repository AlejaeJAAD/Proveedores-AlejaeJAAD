﻿using System;
using System.Collections.Generic;
using grud_backend.Helpers;
using grud_backend.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace grud_backend.Services
{
    public interface IAfiliacionService
    {
        public AfiliacionRequest CreateAfiliacion(string id,Afiliacion afiliacion);
        public Notificaciones CreateNotificacion(string id, Notificacion notificacion);
        public Notificaciones GetNotificaciones(string id);
        public AfiliacionResponse GetAfiliacion(string id);
        public AfiliacionRequest GetAfiliacionIds(string id);
        public AfiliacionResponse UpdateAfiliacion(string id, Afiliacion afiliacion);
        public AfiliacionResponse AceptarAfiliacion(string id, Afiliacion afiliacion);
    }
    public class AfiliacionService:IAfiliacionService
    {
        private readonly IMongoCollection<User> _mongoUsers;
        private readonly IMongoCollection<AfiliacionRequest> _mongoAfiliacion;
        private readonly IMongoCollection<Notificaciones> _mongoNotificaciones;

        
        public AfiliacionService(IAppSettings appSettings)
        {
            var client = new MongoClient(appSettings.ConnectionString);
            var database = client.GetDatabase(appSettings.DatabaseName);
            _mongoAfiliacion = database.GetCollection<AfiliacionRequest>(appSettings.AfiliacionCollectionName);
            _mongoUsers = database.GetCollection<User>(appSettings.UsersCollectionName);
            _mongoNotificaciones = database.GetCollection<Notificaciones>(appSettings.NotificacionesCollectionName);
        }


        public AfiliacionRequest CreateAfiliacion(string id, Afiliacion afiliacion)
        {
            var createdId = Guid.NewGuid().ToString("D");
            var lista = new List<Afiliacion>();
            lista.Add(afiliacion);
            var nuevaAfi = new AfiliacionRequest(createdId, id, lista);
            _mongoAfiliacion.InsertOne(nuevaAfi);
            return nuevaAfi;
        }

        public Notificaciones CreateNotificacion(string id, Notificacion notificacion)
        {
            var noti = _mongoNotificaciones.Find(v => v.Id == id).FirstOrDefault();
            if (noti == null)
            {
                var n = new Notificaciones();
                n.Id = id;
                var nf = new List<Notificacion>();
                nf.Add(notificacion);
                n.LNotificaciones = nf;
                _mongoNotificaciones.InsertOne(n);
                return n;
            }
            noti.LNotificaciones.Add(notificacion);
            _mongoNotificaciones.ReplaceOne(v => v.Id == id, noti);
            return noti;
        }

        public Notificaciones GetNotificaciones(string id)
        {
          return _mongoNotificaciones.Find(v => v.Id == id).FirstOrDefault();
        }

        public AfiliacionResponse GetAfiliacion(string id)
        {
            var afi = 
                _mongoAfiliacion.Find(v => v.SenderId == id).FirstOrDefault();
            if (afi == null)
            {
                return null;
            }
            List<User> nuevaLista = new List<User>();
            for (int i = 0; i < afi.MyAfiliates.Count; i++)
            {
                var recipient = 
                    _mongoUsers.Find(v => v.Id == afi.MyAfiliates[i].RecipientId).FirstOrDefault();
                nuevaLista.Add(recipient);
            }
            AfiliacionResponse response = new AfiliacionResponse();
            response.Id = afi.Id;
            response.SenderId = afi.SenderId;
            response.MyAfiliates = nuevaLista;

            return response;
        }
        public AfiliacionRequest GetAfiliacionIds(string id)
        {
          return  _mongoAfiliacion.Find(v => v.SenderId == id).FirstOrDefault();
        }

        public AfiliacionResponse UpdateAfiliacion(string id, Afiliacion afiliacion)
        {
            var myAfiliates = _mongoAfiliacion.Find(v => v.SenderId == id).FirstOrDefault();
            myAfiliates.MyAfiliates.Add(afiliacion);
            
            _mongoAfiliacion.ReplaceOne(v => v.Id == myAfiliates.Id, myAfiliates);
            List<User> nuevaLista = new List<User>();
            for (int i = 0; i < myAfiliates.MyAfiliates.Count; i++)
            {
                var recipient = 
                    _mongoUsers.Find(v => v.Id == myAfiliates.MyAfiliates[i].RecipientId).FirstOrDefault();
                nuevaLista.Add(recipient);
            }
            AfiliacionResponse response = new AfiliacionResponse();
            response.Id = myAfiliates.Id;
            response.SenderId = myAfiliates.SenderId;
            response.MyAfiliates = nuevaLista;
            return response;
        }

        public AfiliacionResponse AceptarAfiliacion(string id, Afiliacion afiliacion)
        {
            var afi = _mongoAfiliacion.Find(v => v.SenderId == id).FirstOrDefault();
            for (int i = 0; i < afi.MyAfiliates.Count; i++)
            {
                if (afi.MyAfiliates[i].RecipientId == afiliacion.RecipientId)
                {
                    afi.MyAfiliates[i] = afiliacion;
                }

            }
            _mongoAfiliacion.ReplaceOne(v => v.Id == afi.Id, afi);
            List<User> nuevaLista = new List<User>();
            for (int i = 0; i < afi.MyAfiliates.Count; i++)
            {
                var recipient = 
                    _mongoUsers.Find(v => v.Id == afi.MyAfiliates[i].RecipientId).FirstOrDefault();
                    nuevaLista.Add(recipient);
            }
            AfiliacionResponse response = new AfiliacionResponse();
            response.Id = afi.Id;
            response.SenderId = afi.SenderId;
            response.MyAfiliates = nuevaLista;
            return response;

        }
    }
}
