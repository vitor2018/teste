using API.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using static API.Util.Repositorio;

namespace APILocalizaAmigos.Controllers
{
    [RoutePrefix("api/Amigos")]
    public class AmigosController : ApiController
    {
        private LocalizaAmigosEntities db = new RepositorioBase<Usuarios>().Contexto;

        [Route("VerificaCep/{cep}")]
        public bool GetAmigos(string cep)
        {
            return db.Amigos.Count(a => a.NR_CEP.Equals(cep)) > 0;
        }
        
        public IQueryable<Amigos> GetAmigos()
        {
            return db.Amigos;
        }

        [Route("{id}", Name = "GetAmigo")]
        [ResponseType(typeof(Amigos))]
        public IHttpActionResult GetAmigos(int id)
        {
            Amigos amigos = db.Amigos.Find(id);
            if (amigos == null)
            {
                return NotFound();
            }

            return Ok(amigos);
        }
        [Route("GetByIdUsuario/{id}")]
        [ResponseType(typeof(List<Amigos>))]
        public IHttpActionResult GetByIdUsuario(int id)
        {
            List<Amigos> amigos = db.Amigos.Where(a => a.ID_Usuario == id).ToList();
            if (amigos == null)
            {
                return NotFound();
            }

            return Ok(amigos);
        }        

        [Route("{id}/{amigos}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAmigos(int id, Amigos amigos)
        {
            if (id != amigos.ID_Amigo)
            {
                return BadRequest();
            }

            db.Entry(amigos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmigosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [ResponseType(typeof(Amigos))]
        public IHttpActionResult PostAmigos(Amigos amigos)
        {
            db.Amigos.Add(amigos);
            db.SaveChanges();

            return CreatedAtRoute("GetAmigo", new { id = amigos.ID_Amigo }, amigos);
        }

        [Route("{id}")]
        [ResponseType(typeof(Amigos))]
        public IHttpActionResult DeleteAmigos(int id)
        {
            Amigos amigos = db.Amigos.Find(id);
            if (amigos == null)
            {
                return NotFound();
            }

            db.Amigos.Remove(amigos);
            db.SaveChanges();

            return Ok(amigos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AmigosExists(int id)
        {
            return db.Amigos.Count(e => e.ID_Amigo == id) > 0;
        }
    }
}