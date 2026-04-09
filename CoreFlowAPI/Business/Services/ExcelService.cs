using CoreFlowAPI.Business.Interface;
using ClosedXML.Excel;
using System.Reflection;

namespace CoreFlowAPI.Business.Services
{
    public class ExcelService : IExcelService
    {
        public byte[] ExportToExcel<T>(IEnumerable<T> data, string sheetName = "sheet1")
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Header
            for (int i = 0; i < properties.Length; i++) 
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
                worksheet.Cell(1, i +1).Style.Font.Bold = true;
            }
           
            // Rows
            int row = 2;

            foreach (var item in data)
            {
                for (int col = 0; col < properties.Length; col++)
                {
                    var value = properties[col].GetValue(item);
                    worksheet.Cell(row, col + 1).Value = XLCellValue.FromObject(value);
                }
                row++;
            }
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
