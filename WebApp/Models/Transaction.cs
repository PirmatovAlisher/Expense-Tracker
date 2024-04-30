using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public required DateTime CreatedDate { get; set; }

    public TransactionType TransactionType { get; set; }

    public required int CategoryId { get; set; }
    public required Category Category { get; set; }

    public double Amount { get; set; }

    public string? Comment { get; set; }

}
