using ExamApp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamApp.Models
{
    public class Order
	{
		public Order()
		{
			this.Articles = new List<Article>();
			this.BillingAddresses = new List<BillingAddresse>();
			this.Payments = new List<Payment>();
		}

		[Key]
		public long OxId { get; set; }
		public DateTime OrderDatetime { get; set; }
		public EStatus OrderStatus { get; set; }
		public int? InvoiceNumber { get; set; }

		public virtual ICollection<Article> Articles { get; set; }
		public virtual ICollection<BillingAddresse> BillingAddresses { get; set; }
		public virtual ICollection<Payment> Payments { get; set; }
	}
}
