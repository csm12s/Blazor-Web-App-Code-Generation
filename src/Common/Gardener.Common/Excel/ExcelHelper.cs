using MiniExcelLibs;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.Common;

// TODO: 其他项目应该统一从这里引用MiniExcel
//https://gitee.com/dotnetchina/MiniExcel?_from=gitee_search#https://gitee.com/link?target=https%3A%2F%2Fdotnetfiddle.net%2Fw5WD1J


public class ExcelHelper
{
    public static string Extension = ".xlsx";

    #region Read Excel
    public static async Task<DataTable> GetDataTableAsync(string path, bool hasHeader = true)
    {
        var table = await MiniExcel.QueryAsDataTableAsync(path, hasHeader);

        //Change value to string for SqlSugar to get Model list, double to int could cause error
        var newTable = ChangeValueToString(table);

        return newTable;
    }

    public static async Task<List<T>> GetListAsync<T>(string path, string sheetName = null, ExcelType excelType = ExcelType.UNKNOWN, string startCell = "A1", IConfiguration configuration = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class, new()
    {
        var items = await MiniExcel.QueryAsync<T>(path, sheetName, excelType, startCell, configuration, cancellationToken);
        
        return items.ToList();    
    }
    #endregion


    #region Help Function
    public static DataTable ChangeValueToString(DataTable table)
    {
        DataTable newTable = table.Clone(); //just copy structure, no data
        for (int i = 0; i < newTable.Columns.Count; i++)
        {
            if (newTable.Columns[i].DataType != typeof(string))
                newTable.Columns[i].DataType = typeof(string);
        }

        foreach (DataRow row in table.Rows)
        {
            newTable.ImportRow(row);
        }

        return newTable;
    }
    #endregion


}
