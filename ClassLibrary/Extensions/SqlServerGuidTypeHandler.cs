using System.Data;

using Dapper;

namespace ClassLibrary.Extensions;
public class SqlServerGuidTypeHandler : SqlMapper.TypeHandler<string>
{
	public override string Parse(object value)
	{
		if(value is Guid)
		{
			return value.ToString();
		}
		else
		{
			return Convert.ToString(value);
		}
	}

	public override void SetValue(IDbDataParameter parameter, string value)
	{
		parameter.Value = value;
	}
}
