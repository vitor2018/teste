using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using APILocalizaAmigos.Models;

namespace APILocalizaAmigos.Controllers
{
    [RoutePrefix("api/Usuarios")]
    public class UsuariosController : ApiController
    {
        private LocalizaAmigosEntities db = new LocalizaAmigosEntities();    

        [Route("VerificaNome/{nome}")]
        [HttpGet]
        public IHttpActionResult GetUsuarios(string nome)
        {
            try
            {
                return Ok(db.Usuarios.Count(u => u.NM_Usuario.Equals(nome)) > 0);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }            
        }

        [Route("{nome}/{senha}")]
        [ResponseType(typeof(Usuarios))]
        [HttpGet]
        public IHttpActionResult GetUsuarios(string nome, string senha)
        {
            try
            {
                Usuarios usuarios = db.Usuarios.FirstOrDefault(u => u.NM_Usuario.Equals(nome) && u.NM_Senha.Equals(senha));
                if (usuarios == null)
                {
                    return NotFound();
                }

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }            
        }

        [HttpGet]
        public IQueryable<Usuarios> GetUsuarios()
        {
            try
            {
                return db.Usuarios;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        [Route("{id}", Name = "GetUsuario")]
        [ResponseType(typeof(Usuarios))]
        [HttpGet]
        public IHttpActionResult GetUsuarios(int id)
        {
            try
            {
                Usuarios usuarios = db.Usuarios.Find(id);
                if (usuarios == null)
                {
                    return NotFound();
                }

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }            
        }

        [Route("{id}/{usuarios}")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutUsuarios(int id, Usuarios usuarios)
        {
            try
            {
                if (id != usuarios.ID_Usuario)
                {
                    return BadRequest();
                }

                db.Entry(usuarios).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(id))
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
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }            
        }
                
        [ResponseType(typeof(Usuarios))]        
        [HttpPost]
        public IHttpActionResult PostUsuarios(Usuarios usuario)
        {
            try
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();

                return CreatedAtRoute("GetUsuario", new { id = usuario.ID_Usuario }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }            
        }

        [Route("{id}")]
        [ResponseType(typeof(Usuarios))]
        [HttpDelete]
        public IHttpActionResult DeleteUsuarios(int id)
        {
            try
            {
                Usuarios usuarios = db.Usuarios.Find(id);
                if (usuarios == null)
                {
                    return NotFound();
                }

                db.Usuarios.Remove(usuarios);
                db.SaveChanges();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuariosExists(int id)
        {
            return db.Usuarios.Count(e => e.ID_Usuario == id) > 0;
        }
    }
}