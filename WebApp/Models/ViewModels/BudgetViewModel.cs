namespace WebApp.Models.ViewModels;

public class BudgetViewModel
{
    public List<Transaction> Transactions { get; set; }

    public InsertTransactionViewModel InsertTransaction { get; set; }
}
