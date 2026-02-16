using System.Text.Json.Serialization;

namespace CoreFlowSharedLibrary.DTOs
{
    public class UserDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
    }
}
