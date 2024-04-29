using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace WebApp.Models;

public class Transaction
{
    public int TransactionId { get; set; }

    public DateTime CreatedDate { get; set; }

    public TransactionType TransactionType { get; set; }

    public required string CategoryName { get; set; }

    public required Category Category { get; set; }

    public double Amount { get; set; }

    public string? Comment { get; set; }

}
