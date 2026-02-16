using CoreFlowSharedLibrary.Enums;

namespace CoreFlowSharedLibrary.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public TitleOfEmployee Title { get; set; }
        public string FullPersonalId { get { return string.Format("{0}-{1}", PersonalId, PersonalIdLastDigits); } }
        public required int PersonalId { get; set; } 
        public required int PersonalIdLastDigits { get; set; }    
        public string? PhoneNumber { get; set; }
        public CompanyOfEmployee Company { get; set; }
        public string? Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DateOfEmployment { get; set; }    
        public int UserId { get; set; }
      





    }
}
