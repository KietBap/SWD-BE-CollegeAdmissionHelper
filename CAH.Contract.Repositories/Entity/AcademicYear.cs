using CAH.Core.Base;
using System.ComponentModel.DataAnnotations;

namespace CAH.Contract.Repositories.Entity
{
	public class AcademicYear : BaseEntity
	{
		public int Year { get; set; }
		public virtual ICollection<AdmissionInfor> AdmissionInfors { get; set; }
	}
}
