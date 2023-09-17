using CandidatosGrpcService.Models;

namespace CandidatosGrpcService.Factories;

public class CandidatosFactory
{
    internal Candidato ConstruirCandidato(AdicionarCandidatoRequest model)
    {
		if (model is null)
		{
			throw new ArgumentNullException(nameof(model));
		}

		var candidato = new Candidato
        {
            Nome = model.Nome,
            Sobrenome = model.Sobrenome,
            Email = model.Email,
            Telefone = model.Telefone
        };

        return candidato;
    }

    internal Candidato ConstruirCandidato(EditarCandidatoRequest model)
    {
		if (model is null)
		{
			throw new ArgumentNullException(nameof(model));
		}

		var candidato = new Candidato
        {
            Codigo = model.Codigo,
            Nome = model.Nome,
            Sobrenome = model.Sobrenome,
            Email = model.Email,
            Telefone = model.Telefone
        };

        return candidato;
    }
}
