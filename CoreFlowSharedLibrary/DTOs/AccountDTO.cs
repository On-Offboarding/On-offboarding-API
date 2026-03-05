using CoreFlowSharedLibrary.Enums;

namespace CoreFlowSharedLibrary.DTOs
{
    public class AccountDTO
    {
        public string UserName { get; set; }
        public string Info { get; set; }
        public int SystemAccessId { get; set; }
        public StatusOfAccount Status { get; set; }
    }
}
