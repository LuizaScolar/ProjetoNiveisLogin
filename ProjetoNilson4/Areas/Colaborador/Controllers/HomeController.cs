using Microsoft.AspNetCore.Mvc;
using ProjetoNilson4.Libraries.Filtro;
using ProjetoNilson4.Libraries.Login;
using ProjetoNilson4.Models;
using ProjetoNilson4.Models.Constant;
using ProjetoNilson4.Repository.Contract;

namespace ProjetoNilson4.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        private LoginColaborador _loginColaborador;

        public HomeController(IColaboradorRepository repositoryColaborador, LoginColaborador loginColaborador)
        {
            _colaboradorRepository = repositoryColaborador;
            _loginColaborador = loginColaborador;
        }

        [ColaboradorAutorizacao]
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult LoginColaborador()
		{
			return View();
		}


		[HttpPost]
        public IActionResult Login([FromForm] Models.Colaborador colaborador)
        {
            Models.Colaborador colaboradorDB = _colaboradorRepository.Login(colaborador.Email, colaborador.Senha);

                if (colaboradorDB.Tipo == ColaboradorTipoConstant.Gerente)
                {
                    _loginColaborador.Login(colaboradorDB);
                    return new RedirectResult(Url.Action(nameof(Painel)));

                }
               else
                {
                     ViewData["MSG_E"] = "Usuário não encontrado, verifique email e senha digitado";
                     return View();
                }
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

        [ColaboradorAutorizacao]
        public IActionResult Painel()
        {
            return View();
        }

        [ColaboradorAutorizacao]
        public IActionResult Logout()
        {
            _loginColaborador.Logout();
            return RedirectToAction("Login", "Home");
        }
    }
}
