using CAH.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
	public class InforMethod : BaseEntity
	{
		[Required]
		public string AdmissionInforId { get; set; }

		[ForeignKey("AdmissionInforId")]
		public virtual AdmissionInfor AdmissionInfor { get; set; }

		[Required]
		public string AdmissionMethodId { get; set; }

		[ForeignKey("AdmissionMethodId")]
		public virtual AdmissionMethod AdmissionMethod { get; set; }

        public string ScoreType { get; set; }
        public float ScoreRequirement { get; set; }
        public float PercentageOfQuota { get; set; }
    }
}
