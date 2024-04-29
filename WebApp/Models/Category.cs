using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Remote("IsUnique", "Home")]
    public required string Name { get; set; }

}