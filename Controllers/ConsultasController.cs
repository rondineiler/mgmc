using MGMC.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using MGMC.Filters;


namespace MGMC.Controllers
{

    [AuthorizePerfil("Administrador", "Recepcionista", "Medico")]
    public class ConsultasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // LISTAGEM
        public ActionResult Index()
        {
            var consultas = db.Consultas
                              .Include(c => c.Paciente)
                              .Include(c => c.Medico)
                              .ToList();

            return View(consultas);
        }

        // CREATE (GET)
        public ActionResult Create()
        {
            CarregarCombos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Consulta consulta)
        {
            // LIMPA ERROS DE VALIDAÇÃO AUTOMÁTICOS
            ModelState.Remove("Estado");

            if (!ModelState.IsValid)
            {
                CarregarCombos();
                return View(consulta);
            }

            // DEFINE ESTADO FORÇADO (BASE DE DADOS EXIGE)
            consulta.Estado = "Marcada";

            db.Consultas.Add(consulta);
            db.SaveChanges();

            TempData["success"] = "Consulta marcada com sucesso!";
            return RedirectToAction("Index");
        }


        // DELETE
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var consulta = db.Consultas.Find(id);
            if (consulta != null)
            {
                db.Consultas.Remove(consulta);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // MÉTODO AUXILIAR
        private void CarregarCombos()
        {
            ViewBag.Pacientes = new SelectList(
                db.Pacientes.ToList(),
                "Id",
                "Nome"
            );

            ViewBag.Medicos = new SelectList(
                db.Medicos.Where(m => m.Ativo).ToList(),
                "Id",
                "Nome"
            );
        }
    }
}
