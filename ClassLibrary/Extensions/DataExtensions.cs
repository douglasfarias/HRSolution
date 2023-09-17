using System.Data;

namespace ClassLibrary.Extensions;
public static class DataExtensions
{
    public static DataTable FromObject(this DataTable table, object obj)
    {
        var props = obj.GetType().GetProperties();
        for(int index = 0; index < props.Length; index++)
        {
            table.Columns.Add(props[index].Name, props[index].PropertyType);
        }
        
        DataRow row = table.NewRow();
        for(int index = 0; index < props.Length; index++)
        {
            row.SetField(props[index].Name, props[index].GetValue(obj));
        }
        
        table.Rows.Add(row);

        return table;
    }
}
