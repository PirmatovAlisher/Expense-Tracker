using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.HelperModels;

public class DateRangeHelper
{
	[Required(ErrorMessage = "Please enter the value")]
	public DateTime[] Value { get; set; } = [DateTime.Today.AddDays(-6) , DateTime.Today];

	//[NotMapped]
	//public DateTime? StartDate { get; set; }

	//[NotMapped]
	//public DateTime? EndDate { get; set; }
}
