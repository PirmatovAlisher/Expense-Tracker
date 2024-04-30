using Microsoft.EntityFrameworkCore;
using WebApp.Data.Interfaces;
using WebApp.Models;

namespace WebApp.Data;

public class BudgetRepository : IBudgetRepository
{
    private readonly ApplicationDbContext _context;

    public BudgetRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        return await _context.Transactions.Include(t => t.Category).ToListAsync();
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id);
    }

    public async Task<Transaction> MakeTransactionAsync(Transaction transaction)
    {
        if (transaction == null) return null;

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        if (transaction == null) return null;

        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> DeleteTransactionAsync(int id)
    {
        var transaction = _context.Transactions.FirstOrDefault(t => t.TransactionId == id);
        if (transaction == null) return null;

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return transaction;

    }
}

