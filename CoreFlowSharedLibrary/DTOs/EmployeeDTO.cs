using CoreFlowSharedLibrary.Enums;

namespace CoreFlowSharedLibrary.DTOs
{
    public class EmployeeDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public TitleOfEmployee Title { get; set; }
        public required string PersonalId { get; set; }
        public string? PhoneNumber { get; set; }
        public CompanyOfEmployee Company { get; set; }
        public string? Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public List<AccountDTO> Accounts { get; set; }
    }
}
