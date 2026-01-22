using MGMC.Models;
using System.Linq;
using System.Web.Mvc;
using MGMC.Filters;


namespace MGMC.Controllers
{

    [AuthorizePerfil("Administrador")]

    public class MedicosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // LISTAGEM
        public ActionResult Index()
        {
            var medicos = db.Medicos.ToList();
            return View(medicos);
        }

        // CREATE (GET)
        public ActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        // cria médico + utilizador
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medico medico)
        {
            if (!ModelState.IsValid)
                return View(medico);

            //  Criar MÉDICO
            medico.Ativo = true;
            db.Medicos.Add(medico);
            db.SaveChanges();

            //  Criar UTILIZADOR automaticamente 
            var user = new User
            {
                Nome = medico.Nome,
                Email = medico.Email,
                Perfil = "Medico",
                Senha = "123456",
                Ativo = true
            };

            db.Users.Add(user);
            db.SaveChanges();

            TempData["success"] = "Médico e utilizador criados com sucesso!";
            return RedirectToAction("Index");
        }

        // EDIT (GET)
        public ActionResult Edit(int id)
        {
            var medico = db.Medicos.Find(id);
            if (medico == null)
                return HttpNotFound();

            return View(medico);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Medico medico)
        {
            if (!ModelState.IsValid)
                return View(medico);

            var medicoDB = db.Medicos.Find(medico.Id);
            if (medicoDB == null)
                return HttpNotFound();

            medicoDB.Nome = medico.Nome;
            medicoDB.Especialidade = medico.Especialidade;
            medicoDB.Telefone = medico.Telefone;
            medicoDB.Email = medico.Email;
            medicoDB.Ativo = medico.Ativo;

            db.SaveChanges();

            TempData["success"] = "Médico atualizado com sucesso!";
            return RedirectToAction("Index");
        }

        // MOSTRAR
        public ActionResult Mostrar(int id)
        {
            var medico = db.Medicos.Find(id);
            if (medico == null)
                return HttpNotFound();

            return View(medico);
        }

        // DELETE
    
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var medico = db.Medicos.Find(id);
            if (medico != null)
            {
                db.Medicos.Remove(medico);
                db.SaveChanges();

                TempData["success"] = "Médico removido com sucesso!";
            }

            return RedirectToAction("Index");
        }




    }
}
