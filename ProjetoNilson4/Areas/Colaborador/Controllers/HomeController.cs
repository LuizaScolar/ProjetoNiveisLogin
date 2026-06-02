using Microsoft.AspNetCore.Mvc;
using ProjetoNilson4.Libraries.Login;
using ProjetoNilson4.Models;
using ProjetoNilson4.Models.Constant;
using ProjetoNilson4.Repository.Contract;

namespace ProjetoNilson4.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {
        private IColaboradorRepository _repositoryColaborador;
        private LoginColaborador _loginColaborador;

        public HomeController(IColaboradorRepository repositoryColaborador, LoginColaborador loginColaborador)
        {
            _repositoryColaborador = repositoryColaborador;
            _loginColaborador = loginColaborador;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Models.Colaborador colaborador)
        {
            var colaboradorDB = _repositoryColaborador.Login(colaborador.Email, colaborador.Senha);

            if (colaboradorDB != null && !string.IsNullOrEmpty(colaboradorDB.Email) && !string.IsNullOrEmpty(colaboradorDB.Senha))
            {
                if (colaboradorDB.Tipo == ColaboradorTipoConstant.Gerente)
                {
                    _loginColaborador.Login(colaboradorDB);
                    var redirectUrl = Url.Action(nameof(PainelGerente));
                    if (!string.IsNullOrEmpty(redirectUrl))
                    {
                        return new RedirectResult(redirectUrl);
                    }
                    return RedirectToAction(nameof(Index));
                }
                else if (colaboradorDB.Tipo == ColaboradorTipoConstant.Comum)
                {
                    _loginColaborador.Login(colaboradorDB);
                    var redirectUrl = Url.Action(nameof(PainelComum));
                    if (!string.IsNullOrEmpty(redirectUrl))
                    {
                        return new RedirectResult(redirectUrl);
                    }
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["MSG_E"] = "Usuário não encontrado, verifique email e senha digitado";
            return View();
        }


        public IActionResult PainelGerente()
        {
            ViewBag.Nome = _loginColaborador.GetColaborador().Nome;
            ViewBag.Tipo = _loginColaborador.GetColaborador().Tipo;
            ViewBag.Email = _loginColaborador.GetColaborador().Email;
            return View();

        }

        public IActionResult PainelComum()
        {
            ViewBag.Nome = _loginColaborador.GetColaborador().Nome;
            ViewBag.Tipo = _loginColaborador.GetColaborador().Tipo;
            ViewBag.Email = _loginColaborador.GetColaborador().Email;
            return View();

        }
    }
}
