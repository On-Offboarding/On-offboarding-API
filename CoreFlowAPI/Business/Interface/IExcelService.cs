namespace CoreFlowAPI.Business.Interface
{
    public interface IExcelService
    {
        byte[] ExportToExcel<T>(IEnumerable<T> data, string sheetName = "sheet1");
    }
}
