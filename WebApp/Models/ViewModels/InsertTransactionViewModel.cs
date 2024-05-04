using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels;

public class InsertTransactionViewModel
{
    [Key]
    public int TransactionId { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Required]
    public TransactionType TransactionType { get; set; }

    [Required]
    [DisplayName("Category")]
    public int CategoryId { get; set; }
    //public required Category Category { get; set; } = null;

    [Required]
    public double Amount { get; set; }

    [Required]
    public string Comment { get; set; }

    public List<Category> Categories { get; set; }
}
