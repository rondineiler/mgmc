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
    public class PacientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pacientes

        public ActionResult Index()
        {
            var Pacientes = db.Pacientes.ToList();
            return View(Pacientes);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Pacientes.Add(paciente);
                db.SaveChanges();
                TempData["success"] = "Paciente cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(paciente);
        }


        //  UPDATE (GET)
        public ActionResult Edit(int id)
        {
            var paciente = db.Pacientes.Find(id);
            if (paciente == null)
                return HttpNotFound();

            return View(paciente);
        }

        //  UPDATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Paciente atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(paciente);
        }

        //  DELETE
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var paciente = db.Pacientes.Find(id);
            if (paciente != null)
            {
                db.Pacientes.Remove(paciente);
                db.SaveChanges();
                TempData["success"] = "Paciente removido com sucesso!";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Mostrar(int id)
        {
            var paciente = db.Pacientes.Find(id);
            if (paciente == null)
                return HttpNotFound();

            return View(paciente);
        }



    }
}