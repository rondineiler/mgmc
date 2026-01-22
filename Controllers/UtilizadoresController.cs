using MGMC.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MGMC.Filters;

namespace MGMC.Controllers
{

    [AuthorizePerfil("Administrador")]
    public class UtilizadoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // INDEX
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // CREATE (GET)
        public ActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            user.Ativo = true;
            db.Users.Add(user);
            db.SaveChanges();

            TempData["success"] = "Utilizador criado com sucesso!";
            return RedirectToAction("Index");
        }

        // EDIT (GET)
        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // EDIT (POST) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, HttpPostedFileBase FotoUpload)
        {
            if (!ModelState.IsValid)
                return View(user);

            // buscar dados antigos
            var userDB = db.Users
                           .AsNoTracking()
                           .FirstOrDefault(u => u.Id == user.Id);

            if (userDB == null)
                return HttpNotFound();

            // FOTO
            if (FotoUpload != null && FotoUpload.ContentLength > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(FotoUpload.FileName);

                string path = Path.Combine(
                    Server.MapPath("~/Uploads/Users/"),
                    fileName
                );

                FotoUpload.SaveAs(path);
                user.Foto = fileName;
            }
            else
            {
                // mantém foto antiga
                user.Foto = userDB.Foto;
            }

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            TempData["success"] = "Utilizador atualizado com sucesso!";
            return RedirectToAction("Index");
        }

        // MOSTRAR
        public ActionResult Mostrar(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // DELETE
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                TempData["success"] = "Utilizador removido com sucesso!";
            }
            return RedirectToAction("Index");
        }

        // ATIVAR
        [HttpPost]
        public ActionResult Ativar(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                user.Ativo = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // DESATIVAR
        [HttpPost]
        public ActionResult Desativar(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                user.Ativo = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // CHANGE PASSWORD (GET)
        public ActionResult ChangePassword(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        
        // CHANGE PASSWORD (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int id, string NovaSenha, string ConfirmarSenha)
        {
            if (NovaSenha != ConfirmarSenha)
            {
                ModelState.AddModelError("", "As senhas não coincidem.");
                var user = db.Users.Find(id);
                return View(user);
            }

            var userDB = db.Users.Find(id);
            if (userDB == null)
                return HttpNotFound();

            userDB.Senha = NovaSenha;
            db.SaveChanges();

            TempData["success"] = "Senha alterada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
