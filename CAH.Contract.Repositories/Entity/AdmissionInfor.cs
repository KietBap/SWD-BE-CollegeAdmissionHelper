using CAH.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
	public class AdmissionInfor : BaseEntity
	{
		public string UniMajorId { get; set; }
		[ForeignKey("UniMajorId")]
		public virtual UniMajor UniMajor { get; set; }

		public string AcademicYearId { get; set; }
		[ForeignKey("AcademicYearId")]
		public virtual AcademicYear AcademicYear { get; set; }

		public int Quota { get; set; }
        public float ScoreRequirement { get; set; }
        public string ScoreType { get; set; }
		public DateTime AdmisstionDate { get; set; }
		public DateTime Deadline { get; set; }
		public virtual ICollection<InforMethod> InforMethods { get; set; }
		public virtual ICollection<Wishlist> Wishlists { get; set; }
	}
}
