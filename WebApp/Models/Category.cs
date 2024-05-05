using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required]
    [Remote("IsUnique", "Home")]
    public string Name { get; set; } = string.Empty;

}