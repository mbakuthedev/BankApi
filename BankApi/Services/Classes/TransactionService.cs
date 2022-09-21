using BankApi.Models;
using BankApi.Services.Interfaces;
using BankApi.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Services.Classes
{
    public class TransactionService : ITransactionService
    {
        private BankDbContext _dbContext;
        ILogger<TransactionService> _logger;
        private AppSettings _settings;
        private readonly IAccountService _accountService;
        private static string _BankSettlementAccount;

        public TransactionService(BankDbContext dbContext, ILogger<TransactionService> logger, IOptions<AppSettings> settings, IAccountService accountService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _settings = settings.Value;
            _BankSettlementAccount = _settings.BankSettlementAccount;
            _accountService = accountService;
        }
        public Response CreateNewTransactoion(Transaction transaction)
        {
            Response response = new Response();
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created sucessfully";
            response.Data = null;

            return response;

        }

        public Response FindTransactionbyDate(DateTime date)
        {
            Response response = new Response();
            var transaction = _dbContext.Transactions.Where(x => x.TransactionDate == date).ToList();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created sucessfully";
            response.Data = transaction;

            return response;
        }

        public Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            Response response = new Response();
            Account _sourceAccount;
            Account _destinationAccount;
            Transaction transaction = new Transaction();

            //Authenticate User
            var authUser = _accountService.Authenticate(AccountNumber, TransactionPin);
            if (authUser == null) throw new ApplicationException("Invalid Credentials");

            //After Validation
            try
            {
                _sourceAccount = _accountService.GetbyAccountNumber(_BankSettlementAccount);
                _destinationAccount = _accountService.GetbyAccountNumber(AccountNumber);

                //Update the account balances
                _sourceAccount.CurrentAccountBalance -= Amount;
                _destinationAccount.CurrentAccountBalance += Amount;

                //Check if there is an upadte
                if ((_dbContext.Entry(_sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified && (_dbContext.Entry(_destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified)))
                {
                    transaction.TransactionStatus = TranStatus.Sucess;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Sucessful!";
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed!";
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"An ERROR Occured... => {ex.Message}");
            }
            transaction.TransactionType = TranType.Deposit;
            transaction.TransactionSourceAccount = _BankSettlementAccount;
            transaction.TransactionDestinationAccount = AccountNumber;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"TRANSACTION FROM SOURCE => { JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} To => {JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON => { JsonConvert.SerializeObject(transaction.TransactionDate)}";

            //Save Changes
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return response;

        }

        public Response MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            Response response = new Response();
            Account _sourceAccount;
            Account _destinationAccount;
            Transaction transaction = new Transaction();

            //Authenticate User
            var authUser = _accountService.Authenticate(FromAccount, TransactionPin);
            if (authUser == null) throw new ApplicationException("Invalid Credentials");

            //After Validation
            try
            {
                //The money leaves the users account to the BankSettlementAccount
                _sourceAccount = _accountService.GetbyAccountNumber(FromAccount);
                _destinationAccount = _accountService.GetbyAccountNumber(ToAccount);

                //Update the account balances
                //_sourceAccount.CurrentAccountBalance -= Amount;
                _destinationAccount.CurrentAccountBalance -= Amount;

                //Check if there is an upadte
                if ((_dbContext.Entry(_destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified && (_dbContext.Entry(_destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified)))
                {
                    transaction.TransactionStatus = TranStatus.Sucess;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Sucessful!";
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed!";
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"An ERROR Occured... => {ex.Message}");
            }
            transaction.TransactionType = TranType.Transfer;
            transaction.TransactionSourceAccount = FromAccount;
            transaction.TransactionDestinationAccount = ToAccount;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"TRANSACTION FROM SOURCE => { JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} To => {JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON => { JsonConvert.SerializeObject(transaction.TransactionDate)}";

            //Save Changes
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return response;
        }

        public Response MakeWithdrawal(string AccountNumber, decimal Amount, string TransactionPin)
        {
            Response response = new Response();
            Account _sourceAccount;
            Account _destinationAccount;
            Transaction transaction = new Transaction();

            //Authenticate User
            var authUser = _accountService.Authenticate(AccountNumber, TransactionPin);
            if (authUser == null) throw new ApplicationException("Invalid Credentials");

            //After Validation
            try
            {
                //The money leaves the users account to the BankSettlementAccount
                _sourceAccount = _accountService.GetbyAccountNumber(AccountNumber);
                _destinationAccount = _accountService.GetbyAccountNumber(_BankSettlementAccount);

                //Update the account balances
                //_sourceAccount.CurrentAccountBalance -= Amount;
                _destinationAccount.CurrentAccountBalance -= Amount;

                //Check if there is an upadte
                if ((_dbContext.Entry(_destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified && (_dbContext.Entry(_destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified)))
                {
                    transaction.TransactionStatus = TranStatus.Sucess;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Sucessful!";
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed!";
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"An ERROR Occured... => {ex.Message}");
            }
            transaction.TransactionType = TranType.Withdrawal;
            transaction.TransactionSourceAccount = _BankSettlementAccount;
            transaction.TransactionDestinationAccount = AccountNumber;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"TRANSACTION FROM SOURCE => { JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} To => {JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON => { JsonConvert.SerializeObject(transaction.TransactionDate)}";

            //Save Changes
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return response;
        }
    }
}
