using CandidatosGrpcService.Factories;
using CandidatosGrpcService.Models;
using CandidatosGrpcService.Repository;

using Grpc.Core;

using static CandidatosGrpcService.Models.CandidatosService;

namespace CandidatosGrpcService.Services;

public class CandidatosService : CandidatosServiceBase
{
    private readonly CandidatosFactory _factory;
    private readonly CandidatosRepository _repository;
    public CandidatosService(CandidatosFactory factory, CandidatosRepository repository)
    {
        _factory = factory;
        _repository = repository;
    }

	public override Task<ListarCandidatosResponse> ListarCandidatos(ListarCandidatosRequest request, ServerCallContext context)
	{
		var candidatos = _repository.Listar();

		var response = new ListarCandidatosResponse();
		response.Candidatos.AddRange(candidatos);

		return Task.FromResult(response);
	}

	public override Task<AdicionarCandidatoResponse> AdicionarCandidato(AdicionarCandidatoRequest request, ServerCallContext context)
    {
        var candidato = _factory.ConstruirCandidato(request);
        candidato = _repository.Adicionar(candidato);

		var response = new AdicionarCandidatoResponse
		{
			Candidato = candidato
		};
		return Task.FromResult(response);
    }

    public override Task<EditarCandidatoResponse> EditarCandidato(EditarCandidatoRequest request, ServerCallContext context)
    {
        var candidato = _factory.ConstruirCandidato(request);
        candidato = _repository.Editar(candidato);

		var response = new EditarCandidatoResponse
		{
			Candidato = candidato
		};
		return Task.FromResult(response);
    }

    public override Task<BuscarCandidatoResponse> BuscarCandidato(BuscarCandidatoRequest request, ServerCallContext context)
    {
        var candidato = _repository.Buscar(request.Codigo);

		var response = new BuscarCandidatoResponse
		{
			Candidato = candidato
		};
		return Task.FromResult(response);
    }

	public override Task<InativarCandidatoResponse> InativarCandidato(InativarCandidatoRequest request, ServerCallContext context)
	{
		var inativado = _repository.Inativar(request.Codigo);

		var response = new InativarCandidatoResponse
		{
			Inativado = inativado
		};
		return Task.FromResult(response);
	}
}
