using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MGMC.Models;
using MGMC.Filters;



namespace MGMC.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Login");

            ViewBag.TotalUsuarios = db.Users.Count();
            ViewBag.TotalPacientes = db.Pacientes.Count();
            ViewBag.TotalMedicos = db.Medicos.Count();

            return View();
        }

        public ActionResult AcessoNegado()
        {
            return View();
        }


    }
}