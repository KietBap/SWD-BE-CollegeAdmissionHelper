using CAH.Core.Base;

namespace CAH.Contract.Repositories.Entity
{
	public class University : BaseEntity
	{
        public string Name { get; set; }
        public string Location { get; set; }
		public virtual ICollection<UniMajor> UniMajors { get; set; }
	}
}
