using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    [Table("Transactions")]
    public class Transaction
    {
       [Key]
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; }
        public decimal TransactionAmount { get; set; }
        public TranStatus TransactionStatus { get; set; }
        public bool IsSucessful => TransactionStatus.Equals(TranStatus.Sucess);
        public string  TransactionSourceAccount { get; set; }
        public string  TransactionDestinationAccount { get; set; }
        public string TransactionParticulars { get; set; }
        public TranType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public Transaction()
        {
            TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-", "").Substring(1,27)}";
        }
    }
    public enum TranStatus
    {
        Failed,
        Sucess,
        Error
    }
    public enum TranType
    {
        Deposit, 
        Withdrawal,
        Transfer
    }
}
