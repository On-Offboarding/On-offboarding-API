using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.ViewModels
{
    public class CaseReportViewModel
    {
        public DateTime ReportDate { get; set; }
        public required string ReportName { get; set; }
        public required string EmployeeType { get; set; }
        public required string SystemAccessName { get; set; }
        public required string ApprovalName { get; set; }
        public required EmployeeDTO Employee { get; set; }
        public required List<SystemAccessDTO> SystemAccess { get; set; }
    }
}
