namespace ExamApp.Models
{
    public class Article
	{
		public int Id { get; set; }
		public long Nomenclature { get; set; }
		public string Title { get; set; }
		public int Amount { get; set; }
		public double BrutPrice { get; set; }
		public long OrderOxId { get; set; }

		public virtual Order Order { get; set; }
	}
}
