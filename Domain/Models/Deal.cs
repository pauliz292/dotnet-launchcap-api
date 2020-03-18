using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Deal
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Pipeline { get; set; }

        public string DealStage { get; set; }

        public decimal Amount { get; set; }

        public DateTime ClosedDate { get; set; }

        public string DealOwner { get; set; }

        public string DealType { get; set; }

        [MaxLength(250)]
        public string Purpose { get; set; }

        public string LoanTerm { get; set; }

        public int InterestRate { get; set; }

        public decimal CommitmentFee { get; set; }

        public decimal EstablishmentFee { get; set; }

        public decimal ManagementFee { get; set; }

        public decimal BrokerageFee { get; set; }

        public string GoverningLaw { get; set; }

        // foreign keys
        public int BorrowerId { get; set; }

        [ForeignKey("BorrowerId")]
        public virtual Borrower Borrower { get; set; }

        public virtual ICollection<Guarantor> Guarantors { get; set; }
    }
}