using CAH.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
	public class InforMethod : BaseEntity
	{
		public string AdmissionInforId { get; set; }

		[ForeignKey("AdmissionInforId")]
		public virtual AdmissionInfor AdmissionInfor { get; set; }

		public string AdmissionMethodId { get; set; }

		[ForeignKey("AdmissionMethodId")]
		public virtual AdmissionMethod AdmissionMethod { get; set; }
	}
}
