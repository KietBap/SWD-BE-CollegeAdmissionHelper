using CAH.Core.Base;

namespace CAH.Contract.Repositories.Entity
{
	public class AdmissionMethod : BaseEntity
	{
        public string MethodName { get; set; }
        public string Description { get; set; }
		public virtual ICollection<InforMethod> InforMethods { get; set; }
	}
}
