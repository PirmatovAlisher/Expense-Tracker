using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public enum TransactionType
{
    [Display(Name = "Income")]
    Income = 1,

    [Display(Name = "Expense")]
    Expense = 2
}