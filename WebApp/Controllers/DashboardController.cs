using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.HelperModels;

namespace WebApp.Controllers;

public class DashboardController : Controller
{
	private readonly ApplicationDbContext _context;

	public DashboardController(ApplicationDbContext context)
	{
		_context = context;
	}

	DateRangeHelper DateRangeValue = new DateRangeHelper();

	public async Task<IActionResult> Index()
	{

		// Last 7 days Summary
		//DateTime StartDate = DateTime.Today.AddDays(-6);
		//DateTime EndDate = DateTime.Today;

		//DateRangeHelper dateRangeHelper = new ()
		//{
		//	StartDate = DateTime.Today.AddDays(-6),
		//	EndDate = DateTime.Today.AddDays(0)
		//};

		//List<Transaction> SelectedTransactions = await _context.Transactions.
		//	Where(t => t.CreatedDate >= dateRangeHelper.StartDate && t.CreatedDate <= dateRangeHelper.EndDate).
		//	ToListAsync();
		List<Transaction> SelectedTransactions = await _context.Transactions.Include(t => t.Category).
			Where(t => t.CreatedDate >= DateRangeValue.Value[0] && t.CreatedDate <= DateRangeValue.Value[1]).
			ToListAsync();

		// Total Incomes Summary  
		double TotalIncome = SelectedTransactions.
			Where(t => t.TransactionType == TransactionType.Income).
			Sum(j => j.Amount);

		ViewBag.TotalIncome = TotalIncome.ToString("# ### ##0");

		// Total Expense Summary  
		double TotalExpense = SelectedTransactions.
			Where(t => t.TransactionType == TransactionType.Expense).
			Sum(j => j.Amount);

		ViewBag.TotalExpense = TotalExpense.ToString("# ### ##0");

		// Balance Amount 
		var Balance = TotalIncome - TotalExpense;
		ViewBag.Balance = Balance.ToString("# ### ##0");

		//Doughnut Chart - Expense by Category
		ViewBag.DoughnutChartData = SelectedTransactions.
			Where(t => t.TransactionType == TransactionType.Expense).
			GroupBy(j => j.Category.CategoryId).
			Select(k => new
			{
				categoryName = k.First().Category.Name,
				amount = k.Sum(j => j.Amount),
				formatedAmount = k.Sum(j => j.Amount).ToString("# ### ##0"),
			}).
			OrderByDescending(l => l.amount).
			ToList();

		// Spline Chart - Income vs Expense

		// Income
		List<SplineChartData> IncomeSummary = SelectedTransactions.
			Where(t => t.TransactionType == TransactionType.Income).
			GroupBy(j => j.CreatedDate).
			Select(l => new SplineChartData()
			{
				day = l.First().CreatedDate.ToString("dd.MM"),
				income = l.Sum(j => j.Amount)
			}).
			ToList();

		// Expense
		List<SplineChartData> ExpenseSummary = SelectedTransactions.
			Where(t => t.TransactionType == TransactionType.Expense).
			GroupBy(j => j.CreatedDate).
			Select(l => new SplineChartData()
			{
				day = l.First().CreatedDate.ToString("dd.MM"),
				expense = l.Sum(j => j.Amount)
			}).
			ToList();

		// Combine Income & Expense
		int numberOfDays = DateRangeValue.Value[1].Day - DateRangeValue.Value[0].Day;

		string[] Last7Days = Enumerable.Range(0, numberOfDays).
			Select( i => DateRangeValue.Value[0].AddDays(i).
			ToString("dd.MM")).ToArray();

		ViewBag.SplineChartDate = from day in Last7Days
								  join income in IncomeSummary on day equals income.day into dayIncomeJoined
								  from income in dayIncomeJoined.DefaultIfEmpty()
								  join expense in ExpenseSummary on day equals expense.day into expenseJoined
								  from expense in expenseJoined.DefaultIfEmpty()
								  select new
								  {
									  day = day,
									  income = income == null ? 0 : income.income,
									  expense = expense == null ? 0 : expense.expense,
								  };

		//return View(dateRangeHelper);
		return View(DateRangeValue);
	}

	[HttpPost]
	//public async Task<IActionResult> Index([FromForm] DateRangeHelper dateRangeHelper)
	public async Task<IActionResult> Index([FromForm] DateRangeHelper dateRangeHelper)
	{

		if (dateRangeHelper.Value.Length == 0) { return View(DateRangeValue); }

		List<Transaction> SelectedTransactions = await _context.Transactions.Include(t => t.Category).
			Where(t => t.CreatedDate >= dateRangeHelper.Value[0] && t.CreatedDate <= dateRangeHelper.Value[1]).
			ToListAsync();

		// Total Incomes Summary  
		double TotalIncome = SelectedTransactions.
			Where(t => t.TransactionType == TransactionType.Income).
			Sum(j => j.Amount);

		ViewBag.TotalIncome = TotalIncome.ToString("# ### ##0");

		// Total Expense Summary  
		double TotalExpense = SelectedTransactions.
			Where(t => t.TransactionType == TransactionType.Expense).
			Sum(j => j.Amount);

		ViewBag.TotalExpense = TotalExpense.ToString("# ### ##0");

		// Balance Amount 
		var Balance = TotalIncome - TotalExpense;
		ViewBag.Balance = Balance.ToString("# ### ##0");

		//Doughnut Chart - Expense by Category
		ViewBag.DoughnutChartData = SelectedTransactions.
			Where(t => t.TransactionType == TransactionType.Expense).
			GroupBy(j => j.Category.CategoryId).
			Select(k => new
			{
				categoryName = k.First().Category.Name,
				amount = k.Sum(j => j.Amount),
				formatedAmount = k.Sum(j => j.Amount).ToString("# ### ##0"),
			}).
			OrderByDescending(l => l.amount).
			ToList();

		// Spline Chart - Income vs Expense

		// Income
		List<SplineChartData> IncomeSummary = SelectedTransactions.
			Where(t=> t.TransactionType == TransactionType.Income).
			GroupBy(j => j.CreatedDate).
			Select(l => new SplineChartData()
			{
				day = l.First().CreatedDate.ToString("dd.MM"),
				income = l.Sum(j => j.Amount)
			}).
			ToList();

		// Expense
		List<SplineChartData> ExpenseSummary = SelectedTransactions.
			Where(t=> t.TransactionType == TransactionType.Expense).
			GroupBy(j => j.CreatedDate).
			Select(l => new SplineChartData()
			{
				day = l.First().CreatedDate.ToString("dd.MM"),
				expense = l.Sum(j => j.Amount)
			}).
			ToList();

		// Combine Income & Expense
		TimeSpan numberOfDays = dateRangeHelper.Value[1] - dateRangeHelper.Value[0];

		string[] Last7Days = Enumerable.Range(0, numberOfDays.Days).
			Select(i => dateRangeHelper.Value[0].AddDays(i).
			ToString("dd.MM")).ToArray();

		ViewBag.SplineChartDate = from day in Last7Days
								  join income in IncomeSummary on day equals income.day into dayIncomeJoined
								  from income in dayIncomeJoined.DefaultIfEmpty()
								  join expense in ExpenseSummary on day equals expense.day into expenseJoined
								  from expense in expenseJoined.DefaultIfEmpty()
								  select new
								  {
									  day = day,
									  income = income == null ? 0 : income.income,
									  expense = expense == null ? 0 : expense.expense,
								  };



		return View(dateRangeHelper);
	}
}

public class SplineChartData
{
	public string day;
	public double income;
	public double expense;
}