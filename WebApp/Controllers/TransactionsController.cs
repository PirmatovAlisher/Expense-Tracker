using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
	public class TransactionsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public TransactionsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Transactions
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Transactions.Include(t => t.Category);
			return View(await applicationDbContext.ToListAsync());
		}


		// GET: Transactions/AddOrEdit
		public IActionResult AddOrEdit(int id = 0)
		{
			//ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
			//ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
			PopulateCategories();
			if (id == 0)
				return View(new Transaction());
			return View(_context.Transactions.Find(id));
		}

		// POST: Transactions/AddOrEdit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CreatedDate,TransactionType,CategoryId,Amount,Comment")] Transaction transaction)
		{
			if (ModelState.IsValid)
			{
				if(transaction.TransactionId == 0)
					_context.Add(transaction);
				else
					_context.Transactions.Update(transaction);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			//ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", transaction.CategoryId);
			//ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", transaction.CategoryId);
			PopulateCategories();
			return View(transaction);
		}



		// POST: Transactions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var transaction = await _context.Transactions.FindAsync(id);
			if (transaction != null)
			{
				_context.Transactions.Remove(transaction);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		[NonAction]
		public void PopulateCategories()
		{
			var categories = _context.Categories.ToList();
			Category defaultOpttion = new() { CategoryId = 0, Name = "Choose a Category" };
			categories.Insert(0, defaultOpttion);
			ViewBag.Categories = categories;

		}

	}
}
