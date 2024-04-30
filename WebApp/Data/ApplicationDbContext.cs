using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category > Categories { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

public DbSet<WebApp.Models.Dtos.TransactionGetDto> TransactionGetDto { get; set; } = default!;
}
