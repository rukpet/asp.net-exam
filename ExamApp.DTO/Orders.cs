using ExamApp.Enums;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExamApp.Dto
{
    [Serializable()]
	[XmlRoot("orders")]
	public class OrdersDto
	{
		[XmlElement("order")]
		public List<OrderDto> Orders { get; set; }
	}

	[Serializable()]
	public class OrderDto
	{
		[XmlElement("oxid")]
		public long OxId { get; set; }
		[XmlElement("orderdate")]
		public DateTime OrderDatetime { get; set; }
		[XmlElement("orderstatus")]
		public EStatus OrderStatus { get; set; }
		[XmlElement("invoicenumber")]
		public int? InvoiceNumber { get; set; }
		[XmlElement("billingaddress")]
		public BillingAddresseDto BillingAddress { get; set; }

		[XmlArray("articles")]
		[XmlArrayItem("orderarticle", typeof(ArticleDto))]
		public List<ArticleDto> Articles { get; set; }
		[XmlArray("payments")]
		[XmlArrayItem("payment", typeof(PaymentDto))]
		public List<PaymentDto> Payments { get; set; }
	}

	[Serializable()]
	public class ArticleDto
	{
		[XmlElement("artnum")]
		public long Nomenclature { get; set; }
		[XmlElement("title")]
		public string Title { get; set; }
		[XmlElement("amount")]
		public int Amount { get; set; }
		[XmlElement("brutprice")]
		public double BrutPrice { get; set; }
	}

	[Serializable()]
	public class BillingAddresseDto
	{
		[XmlElement("billemail")]
		public string Email { get; set; }
		[XmlElement("billfname")]
		public string Fullname { get; set; }
		[XmlElement("country")]
		public CountryDto Country { get; set; }
		[XmlElement("billcity")]
		public string City { get; set; }
		[XmlElement("billstreet")]
		public string Street { get; set; }
		[XmlElement("billstreetnr")]
		public short HomeNumber { get; set; }
		[XmlElement("billzip")]
		public int Zip { get; set; }
	}

	[Serializable()]
	public class CountryDto
	{
		[XmlElement("geo")]
		public string Geo { get; set; }
	}

	[Serializable()]
	public class PaymentDto
	{
		[XmlElement("method-name")]
		public string MethodName { get; set; }
		[XmlElement("amount")]
		public decimal Amount { get; set; }
	}
}
