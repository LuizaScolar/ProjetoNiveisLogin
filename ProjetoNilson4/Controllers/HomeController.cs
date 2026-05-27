using Microsoft.AspNetCore.Mvc;
using ProjetoNilson4.Libraries.Login;
using ProjetoNilson4.Models;
using ProjetoNilson4.Repository.Contract;
using System.Diagnostics;

namespace ProjetoNilson4.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _clienteRepository;
        private LoginCliente _loginCliente;
        public HomeController(IClienteRepository clienteRepository, LoginCliente loginCliente)
        {
            _clienteRepository = clienteRepository;
            _loginCliente = loginCliente;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clienteDB = _clienteRepository.Login(cliente.Email, cliente.Senha);

            if(clienteDB.Email != null && clienteDB.Senha != null)
            {
                _loginCliente.Login(clienteDB);
                return new RedirectResult(Url.Action(nameof(PainelCliente)));
            }
            else
            {
                // erro na sessão
                ViewData["MSG_E"] = "Usuário não localizado, por favor verifique e-mail e senha digitado";
                return View();
            }


        }

        public IActionResult PainelCliente()
        {
            ViewBag.Nome = _loginCliente.GetCliente().Nome;
            ViewBag.CPF = _loginCliente.GetCliente().CPF;
            ViewBag.Email = _loginCliente.GetCliente().Email;
            //return new ContentResult() { Content = "Este é o Painel do Cliente!};
            return View();
        }
    }
}
