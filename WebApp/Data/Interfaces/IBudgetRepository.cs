using WebApp.Models;

namespace WebApp.Data.Interfaces;

public interface IBudgetRepository
{
    List<Transaction> GetAllTransactionsAsync();

    Transaction GetTransactionByIdAsync(int id);

    Transaction MakeTransactionAsync(Transaction transaction);   

    Transaction UpdateTransactionAsync(int id);

    Transaction DeleteTransactionAsync(int id);
}