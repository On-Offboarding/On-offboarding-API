using CoreFlowAPI.Business.Documents;
using CoreFlowAPI.Business.Interface;
using CoreFlowSharedLibrary.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace CoreFlowAPI.Business.Services
{
    public class PDFService : IPDFService
    {
        private readonly ISystemAccessService _systemAccessService;

        public PDFService(ISystemAccessService accessService)
        {
            _systemAccessService = accessService;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> ExportToPDF(CaseDTO dto)
        {
            var systems = await _systemAccessService.GetAllAsync();
            var accounts = new List<SystemAccessDTO>();

            if (dto.Employee.Accounts?.Count > 0)
            {
                accounts = (from x in dto.Employee.Accounts
                            join y in systems on x.SystemAccessId equals y.Id
                            select new SystemAccessDTO { Id = x.SystemAccessId, Name = y.Name })
                           .ToList();
            }

            return new CaseReportDocument(dto, accounts).GeneratePdf();
        }
    }
}
