public class User{
    [Key, Required]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}