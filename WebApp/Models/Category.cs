using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Remote("IsUnique", "Home")]
    [Required(ErrorMessage = "Name of Category required")]
    public string Name { get; set; } = string.Empty;

}