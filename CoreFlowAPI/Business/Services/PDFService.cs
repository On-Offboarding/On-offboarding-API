using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Business.ViewModels;
using CoreFlowSharedLibrary.DTOs;
using SelectPdf;
using RazorLight;
using System.Net.NetworkInformation;
using System.Text;
namespace CoreFlowAPI.Business.Services
{
    public class PDFService : IPDFService
    {

        private readonly string _pdfFolder = "Templates";
        private readonly ISystemAccessService _systemAccessService;
        public PDFService(ISystemAccessService accessService)
        { 
            _systemAccessService = accessService; 
        }
        public async Task<byte[]> ExportToPDF(CaseDTO dTO)
        {
            var model = await GetPdfModel(dTO);
            string html = await ReplacePlaceholders("caseReport.cshtml", model);
            return ConvertToPdf(html);
        }
        private async Task<CaseReportViewModel> GetPdfModel(CaseDTO dTO)
        {
            var systems = await _systemAccessService.GetAllAsync();
            var accounts = new List<SystemAccessDTO>();
            if (dTO.Employee.Accounts != null && dTO.Employee.Accounts.Count > 0)
            {
                accounts = (from x in dTO.Employee.Accounts
                                join y in systems
                                on x.SystemAccessId equals y.Id
                                select new SystemAccessDTO
                                {
                                    Id = x.SystemAccessId,
                                    Name = y.Name
                                }).ToList();
            }
            
            var model = new CaseReportViewModel
            {
                ReportDate = DateTime.Now,
                ReportName = "Behörighetsrapport",
                EmployeeType = "Anställd",
                SystemAccessName = "Systembehörigheter",
                ApprovalName = "Godkännande",
                Employee = dTO.Employee,
                SystemAccess = accounts,
            }; 
            return model;
        }
        private byte[] ConvertToPdf(string html)
        {
            var converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfStandard = PdfStandard.Full;
            
            var doc = converter.ConvertHtmlString(html);
            var pdfBytes = doc.Save();
            doc.Close();
            return pdfBytes;
        }
        private async Task<string> ReplacePlaceholders(string templateKey, CaseReportViewModel model)
        {
            var path = Path.Combine(AppContext.BaseDirectory, _pdfFolder);
            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(path)
                .Build();
            var html = await engine.CompileRenderAsync(
                templateKey,
                model
                );

            return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(html));


        }

    }
}
