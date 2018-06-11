using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using APILocalizaAmigos.Models;

namespace APILocalizaAmigos.Controllers
{
    public class AmigosController : ApiController
    {
        private LocalizaAmigosEntities db = new LocalizaAmigosEntities();

        [Route("api/Amigos/VerificaCep/{cep}")]
        public bool GetAmigos(string cep)
        {
            return db.Amigos.Count(a => a.NR_CEP.Equals(cep)) > 0;
        }

        [Route("api/Amigos")]
        public IQueryable<Amigos> GetAmigos()
        {
            return db.Amigos;
        }

        [Route("api/Amigos/{id}")]
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

        [Route("api/Amigos/{id}/{amigos}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAmigos(int id, Amigos amigos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        [Route("api/Amigos/{amigos}")]
        [ResponseType(typeof(Amigos))]
        public IHttpActionResult PostAmigos(Amigos amigos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Amigos.Add(amigos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = amigos.ID_Amigo }, amigos);
        }

        [Route("api/Amigos/{id}")]
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