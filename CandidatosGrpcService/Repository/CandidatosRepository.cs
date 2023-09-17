using System.Data;

using CandidatosGrpcService.Models;

using ClassLibrary.Extensions;

using Dapper;

using Microsoft.Data.SqlClient;

namespace CandidatosGrpcService.Repository;

public class CandidatosRepository
{
    private readonly SqlConnection _connection;
    public CandidatosRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    internal IEnumerable<Candidato> Listar()
    {
        return _connection.Query<Candidato>("ListarCandidatos", commandType: CommandType.StoredProcedure);
    }

    internal Candidato Adicionar(Candidato candidato)
    {
        if(candidato is null)
        {
            throw new ArgumentNullException(nameof(candidato));
        }

		var command = new
		{
			candidato.Nome,
			candidato.Sobrenome,
			candidato.Email,
			candidato.Telefone
		};
		var param = new DataTable().FromObject(command).AsTableValuedParameter("AdicionarCandidatoCommand");

        return _connection.QueryFirst<Candidato>("AdicionarCandidato", new { @command = param }, commandType: CommandType.StoredProcedure);
    }

    internal Candidato Buscar(string codigo)
    {
        if(codigo is null)
        {
            throw new ArgumentNullException(nameof(codigo));
        }

		var command = new
		{
			Codigo = codigo
		};
		var param = new DataTable().FromObject(command).AsTableValuedParameter("BuscarCandidatoCommand");

		return _connection.QueryFirst<Candidato>("BuscarCandidato", new { @command = param }, commandType: CommandType.StoredProcedure);
    }

    internal Candidato Editar(Candidato candidato)
    {
        if(candidato is null)
        {
            throw new ArgumentNullException(nameof(candidato));
        }

		var command = new
		{
			candidato.Codigo,
			candidato.Nome,
			candidato.Sobrenome,
			candidato.Email,
			candidato.Telefone
		};
		var param = new DataTable().FromObject(command).AsTableValuedParameter("EditarCandidatoCommand");

		return _connection.QueryFirst<Candidato>("EditarCandidato", new { @command = param }, commandType: CommandType.StoredProcedure);
    }

    internal int Inativar(string codigo)
    {
        if(codigo is null)
        {
            throw new ArgumentNullException(nameof(codigo));
        }

		var command = new
		{
			Codigo = codigo
		};
		var param = new DataTable().FromObject(command).AsTableValuedParameter("InativarCandidatoCommand");

		return _connection.Execute("InativarCandidato", new { @command = param }, commandType: CommandType.StoredProcedure);
    }
}
