namespace CRUD_Entity_Framework_Core_MVC_01.Entities;

using System.Text.Json.Serialization;

public class Song{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Artist { get; set; }

    [JsonIgnore]
    public string? PasswordHash { get; set; }
} 