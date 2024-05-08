using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

public class Transaction
{
	[Key]
	public int TransactionId { get; set; }

	[Required]
	public DateTime CreatedDate { get; set; } = DateTime.Now;

	public TransactionType TransactionType { get; set; }
	[Required, Range(1, int.MaxValue,ErrorMessage = "Please select a category.")]
	public int CategoryId { get; set; }
	public Category? Category { get; set; }

	[Required, Range(1, int.MaxValue, ErrorMessage = "Amount should be greater then zero.")]
	public double Amount { get; set; }

	public string? Comment { get; set; } = String.Empty;

	[NotMapped] 
	public string? CategoryName 
	{ get
		{
			return this.Category?.Name ?? String.Empty;
		}
	}

	[NotMapped]
	public string? FormatedAmount
	{
		get
		{
			return (TransactionType == TransactionType.Income ? $"+ "  : $"- ") + Amount.ToString("# ### ##0.00");
		}
	}

	[NotMapped]
	public string? FormatedTransactionType
	{
		get
		{
			return TransactionType == TransactionType.Income ? "Income" : "Expense";
		}
	}

}
