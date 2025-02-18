using CAH.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
	public class UniMajor : BaseEntity
	{
		public string MajorId { get; set; }

		[ForeignKey("MajorId")]
		public virtual Major Major { get; set; }

		public string UniversityId { get; set; }

		[ForeignKey("UniversityId")]
		public virtual University University { get; set; }

		public virtual ICollection<AdmissionInfor> AdmissionInfors { get; set; }
	}
}
