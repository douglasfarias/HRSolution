namespace CandidatosGrpcService.Models;

public partial class Candidato
{
	public static explicit operator EditarCandidatoRequest(Candidato model)
	{
		return new EditarCandidatoRequest
		{
			Codigo = model.Codigo,
			Nome = model.Nome,
			Sobrenome = model.Sobrenome,
			Email = model.Email,
			Telefone = model.Telefone,
		};
	}
}
