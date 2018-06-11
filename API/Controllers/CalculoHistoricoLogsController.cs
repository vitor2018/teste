using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;
using static API.Util.Repositorio;

namespace API.Controllers
{
    [RoutePrefix("api/CalculoHistoricoLogs")]
    public class CalculoHistoricoLogsController : ApiController
    {
        private LocalizaAmigosEntities db = new RepositorioBase<Usuarios>().Contexto;

        [Authorize]
        [Route("{idUsuario}/{idAmigo}")]
        [HttpGet]
        public string GetCalculoHistoricoLog(int idUsuario, int idAmigo)
        {
            try
            {
                Amigos amigoSelecionado = db.Amigos.FirstOrDefault(a => a.ID_Amigo == idAmigo);
                List<Amigos> amigos = db.Amigos.Where(a => a.ID_Usuario == idUsuario && a.ID_Amigo != idAmigo).ToList();
                string amigosProximos = "";
                Dictionary<string, double> distancias = new Dictionary<string, double>();
                double aux = 0;

                amigos.ForEach(a => {
                    aux = CalculaDistanciaPontos(Convert.ToDouble(amigoSelecionado.NR_Lat), Convert.ToDouble(amigoSelecionado.NR_Lng), Convert.ToDouble(a.NR_Lat), Convert.ToDouble(a.NR_Lng));
                    distancias.Add(a.NM_Amigo, aux);
                    db.Set<CalculoHistoricoLog>().Add(new CalculoHistoricoLog()
                    {
                        ID_Usuario = idUsuario,
                        NM_Calculo = aux.ToString(),
                        DT_Criacao = DateTime.Now
                    });
                    db.SaveChanges();
                });

                distancias.OrderBy(a => a.Value).Take(3).ToList().ForEach(d => {
                    amigosProximos = string.Concat(amigosProximos, string.Format("{0} ", d.Key));
                });

                if (distancias == null || distancias.Count == 0)
                    amigosProximos = "Nenhum amigo encontrado.";

                return amigosProximos;
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError).ToString();
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

        private double CalculaDistanciaPontos(double pxIni, double pyIni, double pxFim, double pyFim)
        {            
            return Math.Sqrt(Math.Pow(pxIni - pxFim, 2) + Math.Pow(pyIni - pyFim, 2));
        }

        private bool CalculoHistoricoLogExists(int id)
        {
            return db.CalculoHistoricoLog.Count(e => e.ID_CalculoHistoricoLog == id) > 0;
        }
    }
}