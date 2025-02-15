using CAH.Core.Base;
using System.ComponentModel.DataAnnotations;

namespace CAH.Contract.Repositories.Entity
{
	public class Shop : BaseEntity
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(255)]
		public string Address { get; set; }

		[MaxLength(100)]
		public string ShopEmail { get; set; }

		[MaxLength(20)]
		public string ShopPhone { get; set; }

		[Required]
		public TimeSpan OpenTime { get; set; }

		[Required]
		public TimeSpan CloseTime { get; set; }

		[MaxLength(100)]
		public string Title { get; set; }

		public string? ShopImage { get; set; }

		public virtual ICollection<Service> Services { get; set; }
	}
}
