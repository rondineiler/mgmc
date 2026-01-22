using MGMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MGMC.Filters;



namespace MGMC.Controllers
{

    [AuthorizePerfil("Administrador", "Medico")]
    public class HorariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //  LISTAGEM
        public ActionResult Index()
        {
            var horarios = db.Horarios
                             .Include(h => h.Medico)
                             .ToList();

            return View(horarios);
        }

        //  CREATE (GET)
        public ActionResult Create()
        {
            ViewBag.Medicos = new SelectList(
                db.Medicos.Where(m => m.Ativo).ToList(),
                "Id",
                "Nome"
            );

            return View();
        }

        //  CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Horario horario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Medicos = new SelectList(
                    db.Medicos.Where(m => m.Ativo).ToList(),
                    "Id",
                    "Nome",
                    horario.MedicoId
                );
                return View(horario);
            }

            // Validação lógica
            if (horario.HoraFim <= horario.HoraInicio)
            {
                ModelState.AddModelError("", "A hora de fim deve ser maior que a hora de início.");

                ViewBag.Medicos = new SelectList(
                    db.Medicos.Where(m => m.Ativo).ToList(),
                    "Id",
                    "Nome",
                    horario.MedicoId
                );

                return View(horario);
            }

            db.Horarios.Add(horario);
            db.SaveChanges();

            TempData["success"] = "Horário cadastrado com sucesso!";
            return RedirectToAction("Index");
        }

        //  DELETE
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var horario = db.Horarios.Find(id);
            if (horario != null)
            {
                db.Horarios.Remove(horario);
                db.SaveChanges();
                TempData["success"] = "Horário removido com sucesso!";
            }

            return RedirectToAction("Index");
        }

 

    }
}