namespace CRUD_Entity_Framework_Core_MVC_01.Models.Songs;

using System.ComponentModel.DataAnnotations;
using CRUD_Entity_Framework_Core_MVC_01.Entities;

public class CreateRequest{
    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Artist { get; set; }
}