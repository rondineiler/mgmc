using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MGMC.Filters
{
    public class AuthorizePerfilAttribute : ActionFilterAttribute
    {
        private readonly string[] _perfisPermitidos;

        public AuthorizePerfilAttribute(params string[] perfis)
        {
            _perfisPermitidos = perfis;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = HttpContext.Current.Session;

            if (session["Perfil"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
                return;
            }

            string perfil = session["Perfil"].ToString();

            if (!_perfisPermitidos.Contains(perfil))
            {
                filterContext.Result = new RedirectResult("~/Home/AcessoNegado");
            }
        }
    }
}
