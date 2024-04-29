using Microsoft.EntityFrameworkCore;
using WebApp.Data.Interfaces;
using WebApp.Models;

namespace WebApp.Data;

public class BudgetRepository
{
    private readonly ApplicationDbContext _context;

    public BudgetRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}

