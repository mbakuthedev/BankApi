using AutoMapper;
using BankApi.Models;
using BankApi.Services;
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
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper )
        {
            _accountService = accountService;
            _mapper = mapper;
                
        }
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterNewAccount([FromBody]RegisterNewAccountModel newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(newAccount);
            var account = _mapper.Map<Account>(newAccount);
            return Ok(_accountService.Create(account, newAccount.Pin, newAccount.ConfirmedPin));
        }
        [HttpGet]
        [Route("getAllAccounts")]
        public IActionResult GetAllAccounts()
        {
            var accounts = _accountService.GetAllAccounts();
            var cleanedAccounts = _mapper.Map<IList<GetAccountModel>> (accounts);
            return Ok(cleanedAccounts);
        }
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var account = _accountService.Authenticate(model.AccountNumber, model.Pin);
            return Ok(account);
        }
        [HttpGet]
        [Route("get_account_by_account_number")]
        public IActionResult GetByAccountNumber(string AccountNumber)
        {
            if (Regex.IsMatch(AccountNumber, @"^[0]{1-9}\d(9)$|^[0]{1-9}\d(9)$")) return BadRequest("Account number must be 10-digits");
            var account = _accountService.GetbyAccountNumber(AccountNumber);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);

        }
        [HttpGet]
        [Route("get_account_by_id")]
        public IActionResult GetAccountById(int Id)
        {
            var account = _accountService.GetbyId(Id);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);

        }
        [HttpPost]
        [Route("update")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var account = _mapper.Map<Account>(model);
            _accountService.Update(account, model.Pin);
            return Ok();
        }
    }
}
