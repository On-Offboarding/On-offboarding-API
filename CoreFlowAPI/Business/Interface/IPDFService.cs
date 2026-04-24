using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Interface
{
    public interface IPDFService
    {
       Task<byte[]> ExportToPDF(CaseDTO data);
    }
}
