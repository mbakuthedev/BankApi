using AutoMapper;
using BankApi.Models;
using BankApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _transactionService;
        IMapper _mapper;
        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("crete_new_transaction")]
        public IActionResult CreateNewTransaction([FromBody] TransactionRequestDto newTransaction)
        {
            if (!ModelState.IsValid) return BadRequest(newTransaction);
            var transaction = _mapper.Map<Transaction>(newTransaction);
            return Ok(_transactionService.CreateNewTransactoion(transaction));
        }
        [HttpPost]
        [Route("make_deposit")]
        public IActionResult MakeDeposit(string AccountNumber, decimal Amount, string Pin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0]/[1-9]\d[9]$")) return BadRequest("Account Number must be 10digits") ;
            return Ok(_transactionService.MakeDeposit(AccountNumber, Amount, Pin));
        }
        [HttpPost]
        [Route("make_withdrawal")]
        public IActionResult MakeWithdrawal(string AccountNumber, decimal Amount, string Pin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0]/[1-9]\d[9]$")) return BadRequest("Account Number must be 10digits");
            return Ok(_transactionService.MakeWithdrawal(AccountNumber, Amount, Pin));
        }
        [HttpPost]
        [Route("make_funds_transfer")]
        public IActionResult Transfer(string ToAccount, string FromAccount, decimal Amount, string Pin)
        {
            if (!Regex.IsMatch(FromAccount, @"^[0]/[1-9]\d[9]$") || !Regex.IsMatch(ToAccount, @"^[0]/[1-9]\d[9]$")) return BadRequest("Account Number must be 10digits");
            return Ok(_transactionService.MakeFundsTransfer(FromAccount, ToAccount, Amount, Pin));
        }
    }
}
