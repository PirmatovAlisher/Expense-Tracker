using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Dtos;

public class TransactionGetDto
{
    [Key]
    public int TransactionId { get; set; }

    public DateTime CreatedDate { get; set; }

    public TransactionType TransactionType { get; set; }

    public Category Category { get; set; }

    public double Amount { get; set; }

    public string? Comment { get; set; }
}
