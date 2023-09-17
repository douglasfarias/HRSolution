using System.Diagnostics;

using CandidatosGrpcService.Models;

using CandidatosWebApplication.Models;

using Microsoft.AspNetCore.Mvc;

using static CandidatosGrpcService.Models.CandidatosService;

namespace CandidatosWebApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CandidatosServiceClient _client;

    public HomeController(ILogger<HomeController> logger, CandidatosServiceClient client)
    {
        _logger = logger;
        _client = client;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Listar(ListarCandidatosRequest request)
    {
        var response = await _client.ListarCandidatosAsync(request);

        return View(response.Candidatos);
    }

    [HttpGet]
    public IActionResult Adicionar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromForm] AdicionarCandidatoRequest request)
    {
        var response = await _client.AdicionarCandidatoAsync(request);
		ViewBag.Info = "Candidato adicionado com sucesso!";
        return View(nameof(Editar), (EditarCandidatoRequest)response.Candidato);
    }

    [HttpGet]
    public async Task<IActionResult> Detalhes([FromRoute] string codigo)
    {
        var request = new BuscarCandidatoRequest { Codigo = codigo };
        var response = await _client.BuscarCandidatoAsync(request);

        return View(response.Candidato);
    }

    [HttpGet]
    public async Task<IActionResult> Editar([FromQuery] string codigo)
    {
        var request = new BuscarCandidatoRequest { Codigo = codigo.ToString() };
        var response = await _client.BuscarCandidatoAsync(request);

        return View((EditarCandidatoRequest)response.Candidato);
    }

    [HttpPost]
    public async Task<IActionResult> Editar([FromForm] EditarCandidatoRequest request)
    {
        var response = await _client.EditarCandidatoAsync(request);
		ViewBag.Info = "Candidato editado com sucesso!";
		return View((EditarCandidatoRequest)response.Candidato);
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
