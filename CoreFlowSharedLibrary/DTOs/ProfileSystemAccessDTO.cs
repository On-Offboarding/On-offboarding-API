namespace CoreFlowSharedLibrary.DTOs
{
    public class ProfileSystemAccessDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<SystemAccessDTO> SystemAccesses { get; set; }
    }
}
