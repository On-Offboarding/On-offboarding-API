using CoreFlowSharedLibrary.Enums;

namespace CoreFlowSharedLibrary.DTOs
{
    public class CaseDTO
    {
        public required EmployeeDTO Employee { get; set; }
        public TypeOfCase Type { get; set; }
        public StatusOfCase Status { get; set; }
        public int CreatedByUser { get; set; }
    }
}
