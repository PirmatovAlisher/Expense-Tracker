using WebApp.Models;

namespace WebApp.Data.Interfaces;

public interface IBudgetRepository
{
    Task<List<Transaction>> GetAllTransactionsAsync();

    Task<Transaction> GetTransactionByIdAsync(int id);

    Task<Transaction> MakeTransactionAsync(Transaction transaction);   

    Task<Transaction> UpdateTransactionAsync(Transaction transaction);

    Task<Transaction> DeleteTransactionAsync(int id);
}