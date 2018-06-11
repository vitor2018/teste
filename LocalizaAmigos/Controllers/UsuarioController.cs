using LocalizaAmigos.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LocalizaAmigos.Controllers
{
    public class UsuarioController : Controller
    {
        #region Properties
        private int? ID_Usuario
        {
            get { return (int)Session["ID_Usuario"]; }
            set { Session["ID_Usuario"] = value; }
        }
        #endregion
        #region Actions
        public ActionResult Index()
        {
            Session.Abandon();
            ID_Usuario = 0;
            ViewBag.Confirma = 0;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Usuario model, FormCollection form)
        {
            try
            {
                ViewBag.Confirma = 0;
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync(string.Format("Usuarios/{0}/{1}", model.NM_Usuario.Trim(), model.NM_Senha.Trim())).Result;
                Usuario usuario = response.Content.ReadAsAsync<Usuario>().Result;
                if (usuario == null)
                {
                    ViewBag.Confirma = -1;
                    return View(model);
                }
                ViewBag.Confirma = 1;
                ID_Usuario = usuario.ID_Usuario;

                return RedirectToAction("Amigos", "Usuario", new { area = "" });
            }
            catch (Exception ex)
            {
                ViewBag.Confirma = -2;
                return View(model);
            }
        }

        public ActionResult Cadastro()
        {
            ViewBag.Confirma = 0;
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(Usuario model, FormCollection form)
        {
            try
            {
                ViewBag.Confirma = 0;
                HttpResponseMessage verificaNome = GlobalVariables.WebApiClient.GetAsync(string.Format("Usuarios/VerificaNome/{0}", model.NM_Usuario)).Result;
                if (verificaNome.Content.ReadAsAsync<bool>().Result)
                {
                    ViewBag.Confirma = -1;
                    return View(model);
                }

                HttpResponseMessage cadastro = GlobalVariables.WebApiClient.PostAsJsonAsync("Usuarios", model).Result;
                ViewBag.Confirma = 1;

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Confirma = -2;
                return View(model);
            }
        }

        public ActionResult Amigos()
        {
            try
            {                
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync(string.Format("Amigos/GetByIdUsuario/{0}", ID_Usuario ?? 0)).Result;
                ViewBag.Amigos = response.Content.ReadAsAsync<List<Amigo>>().Result;
                return View();
            }
            catch (Exception ex)
            {                
                return RedirectToAction("Index", "Usuario", new { area = "" });
            }
        }
        #endregion
        #region Methods
        [HttpPost]
        public JsonResult SalvaAmigo(int idUsuario, string nome, string cep, string nrLat, string nrLng)
        {
            try
            {
                HttpResponseMessage verificaCep = GlobalVariables.WebApiClient.GetAsync(string.Format("Amigos/VerificaCep/{0}", cep)).Result;
                if (verificaCep.Content.ReadAsAsync<bool>().Result)
                    return Json(new { msg = "CEP já cadastrado.", isSuccess = false }, JsonRequestBehavior.AllowGet);

                Amigo amigo = new Amigo()
                {
                    ID_Usuario = idUsuario,
                    NM_Amigo = nome,
                    NR_CEP = cep,
                    NR_Lat = Decimal.Parse(nrLat.Replace('.', ',')),
                    NR_Lng = Decimal.Parse(nrLng.Replace('.', ','))
                };

                HttpResponseMessage cadastro = GlobalVariables.WebApiClient.PostAsJsonAsync("Amigos", amigo).Result;               
                return Json(new { msg = "", id = cadastro.Content.ReadAsAsync<Amigo>().Result.ID_Amigo, isSuccess = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message, isSuccess = false }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}