using Microsoft.AspNetCore.Mvc;
using ProjetoNilson4.Models.Constant;
using ProjetoNilson4.Repository.Contract;

namespace ProjetoNilson4.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;

        public string Situacao { get; private set; }

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View(_clienteRepository.ObterTodosClientes());
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] ClienteController cliente)
        {
            cliente.Situacao = SituacaoConstant.Ativo;

            _clienteRepository.Cadastrar(cliente);
            return RedirectToAction(nameof(Cadastrar));
        }

    }
}
