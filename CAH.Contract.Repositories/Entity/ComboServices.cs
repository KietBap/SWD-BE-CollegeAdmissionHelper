using CAH.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
	public class ComboServices : BaseEntity
	{
		public string ServiceId { get; set; }

		[ForeignKey("ServiceId")]
		public virtual Service Service { get; set; }

		public string ComboId { get; set; }

		[ForeignKey("ComboId")]
		public virtual Combo Combo { get; set; }
	}
}
