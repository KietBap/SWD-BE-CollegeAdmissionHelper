using CAH.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
	public class UniMajor : BaseEntity
	{
		[Required]
		public string MajorId { get; set; }

		[ForeignKey("MajorId")]
		public virtual Major Major { get; set; }

		[Required]
		public string UniversityId { get; set; }

		[ForeignKey("UniversityId")]
		public virtual University University { get; set; }
        public string? TuitionFee { get; set; }
        public string? MajorCode { get; set; }

        public virtual ICollection<AdmissionInfor> AdmissionInfors { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
	}
}
