using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Data.Interfaces;
using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IMapper _mapper;

        public HomeController(IBudgetRepository budgetRepository, IMapper mapper)
        {
            _budgetRepository = budgetRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _budgetRepository.GetAllTransactionsAsync();

            var mapped = _mapper.Map<List<TransactionGetDto>>(transactions);

            return View(mapped);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionGetDto transactionGetDto)
        {
            if(ModelState.IsValid)
            {
                var transaction = _mapper.Map<Transaction>(transactionGetDto);
                await _budgetRepository.MakeTransactionAsync(transaction);
                return View(transaction);
            }
            return BadRequest();
        }


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
