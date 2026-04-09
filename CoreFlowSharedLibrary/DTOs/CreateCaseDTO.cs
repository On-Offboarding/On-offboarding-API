using CoreFlowSharedLibrary.Enums;

namespace CoreFlowSharedLibrary.DTOs
{
    public class CreateCaseDTO
    {
        public required CreateEmployeeDTO Employee { get; set; }
        public TypeOfCase Type { get; set; }
        public StatusOfCase Status { get; set; }
        public int CreatedByUser { get; set; }
    }
}
