using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionAzureStorage;

namespace SubirFicheros.Controllers
{
    public class FicherosController : Controller
    {

        

        // GET: Ficheros
        public ActionResult Index()
        {
            var cuenta = ConfigurationManager.AppSettings["cuentaAzure"].ToString();
            var contenedor = ConfigurationManager.AppSettings["contenedorAzure"].ToString();
            var key = ConfigurationManager.AppSettings["keyAzure"].ToString();

            var azure = new AlmacenamientoAzure(cuenta, key);

            return View(azure.Listar(contenedor));
        }

        public ActionResult Nueva()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nueva(HttpPostedFileBase foto)
        {
            var cuenta = ConfigurationManager.AppSettings["cuentaAzure"].ToString();
            var contenedor = ConfigurationManager.AppSettings["contenedorAzure"].ToString();
            var key = ConfigurationManager.AppSettings["keyAzure"].ToString();

            if (foto != null && foto.ContentLength > 0)
            {
                var nombre = DateTime.Now.Ticks.ToString();
                nombre += "." + foto.FileName.
                    Substring(foto.FileName.LastIndexOf(".") + 1);

                var azure = new AlmacenamientoAzure(cuenta, key);
                azure.SubirObjeto(foto.InputStream,nombre,contenedor);
            
            }

            return RedirectToAction("Index");
        }
    }
}