using CandidatosGrpcService.Models;

namespace CandidatosWebApplication.Factories;

public class CandidatosFactory
{
    public static EditarCandidatoRequest CriarEditarCandidatoRequest(Candidato candidato)
    {
        return new EditarCandidatoRequest
        {
            Codigo = candidato.Codigo,
            Nome = candidato.Nome,
            Sobrenome = candidato.Sobrenome,
            Email = candidato.Email,
            Telefone = candidato.Telefone
        };
    }
}
