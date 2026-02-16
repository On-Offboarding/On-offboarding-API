namespace CoreFlowSharedLibrary.DTOs
{
    public class ProfileSystemAccessDTO
    {
        public string? Name { get; set; }
        public List<SystemAccessDTO> SystemAccesses { get; set; }
    }
}
