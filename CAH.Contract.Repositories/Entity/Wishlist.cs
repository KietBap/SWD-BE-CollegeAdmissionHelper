using CAH.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAH.Contract.Repositories.Entity
{
	public class Wishlist : BaseEntity
	{
		[Required]
		public Guid UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		public string AdmissionInforId { get; set; }

		[ForeignKey("AdmissionInforId")]
		public virtual AdmissionInfor AdmissionInfor { get; set; }
	}
}
