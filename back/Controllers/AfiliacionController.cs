using System;
using grud_backend.Models;
using grud_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grud_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AfiliacionController: ControllerBase
    {        private IAfiliacionService _afiliacionService;

        public AfiliacionController(IAfiliacionService afiliacionService)
        {
            _afiliacionService = afiliacionService;
        }
        [HttpPost("{id}")]
        public ActionResult CreateAfiliacion(string id, [FromBody] Afiliacion afiliacion)
        {
            var afi = _afiliacionService.GetAfiliacion(id);
            if (afi != null)
            {
                UpdateAfiliacion(id, afiliacion);
            }

            var af = _afiliacionService.GetAfiliacionIds(afiliacion.RecipientId);
            bool notificacion = true;
            if (af != null)
            {
                foreach (var user in af.MyAfiliates)
                {
                    if (user.RecipientId == id) notificacion = false;
                }
            }


            var nAfi = _afiliacionService.CreateAfiliacion(id, afiliacion);
            var n = new Notificacion();
            n.Id = nAfi.SenderId;
            n.Tipo = 1;
            if (notificacion)
            {
                _afiliacionService.CreateNotificacion(afiliacion.RecipientId, n);
            }
            return Ok(new {afiliacion=nAfi,msg="Notifiacion enviada"});
        }
        [HttpGet("{id}")]
        public ActionResult GetAfiliacion(string id)
        {
            var afi = _afiliacionService.GetAfiliacion(id);
            if (afi == null)
            {
                return Ok(new {error = new {msg = "No se encontró"}});
            }
            return Ok(afi);
        }
        [HttpPut("aceptar/{recipient}/{sender}")]
        public ActionResult AcceptAfiliacion(string recipient,string sender)
        {
            var afi = _afiliacionService.GetAfiliacionIds(sender);
            if (afi == null)
            {
                return Ok("No existe la afiliacion");
            }
            Afiliacion afiliacion = new Afiliacion();
            bool bandera = false;
            for (int i = 0; i < afi.MyAfiliates.Count; i++)
            {
                if (afi.MyAfiliates[i].RecipientId == recipient)
                {
                    bandera = true;
                    afiliacion = afi.MyAfiliates[i];
                    afiliacion.Aceptado = true;
                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
                    afiliacion.FechaAfiliacion = DateTime.Today.AddHours(1).AddMinutes(25);
                    afiliacion.RecipientId = recipient;
                }
            }

            if (bandera)
            {
                var nAfi = new Afiliacion();
                nAfi.RecipientId = sender;
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
                nAfi.FechaAfiliacion = DateTime.Today.AddHours(1).AddMinutes(25);
                nAfi.Aceptado = true;
                CreateAfiliacion(recipient, nAfi);
            }

            return bandera 
                ? 
                Ok(_afiliacionService.AceptarAfiliacion(sender, afiliacion)) 
                : 
                Ok("No existe el usuario en los afiliados!");
        }

        [HttpGet("notificaciones/{id}")]
        public ActionResult GetNotificaciones(string id)
        {
            var notifiaciones = _afiliacionService.GetNotificaciones(id);
            if (notifiaciones == null)
            {
                return Ok();
            }
            return Ok(notifiaciones);
        }

        [HttpPut("{id}",Name = "Update")]
        public ActionResult UpdateAfiliacion(string id ,[FromBody] Afiliacion nuevaAfiliacion)
        {
            var afi = _afiliacionService.GetAfiliacion(id);
            if (afi == null)
            {
                return Ok(new {error = new {msg = "No se encontró"}});
            }

            return Ok(_afiliacionService.UpdateAfiliacion(id, nuevaAfiliacion));
        }
    }
}
