using MGMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MGMC.Filters;


namespace MGMC.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Senha)
        {
            var user = db.Users
                .FirstOrDefault(u => u.Email == Email && u.Senha == Senha);

            if (user == null)
            {
                ViewBag.Error = "Email ou senha inválidos";
                return View();
            }

            if (!user.Ativo)
            {
                ViewBag.Error = "Utilizador inativo";
                return View();
            }

            // Criar Sessão
            Session["UserId"] = user.Id;
            Session["UserNome"] = user.Nome;
            Session["Perfil"] = user.Perfil;


            return RedirectToAction("Index", "Home");
        }

        // LOGOUT
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}