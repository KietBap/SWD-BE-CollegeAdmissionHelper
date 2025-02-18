using CAH.Core.Base;

namespace CAH.Contract.Repositories.Entity
{
	public class Major : BaseEntity
	{
        public string Name { get; set; }
        public string RelatedSkills { get; set; }
        public string Description { get; set; }
		public virtual ICollection<UniMajor> UniMajors { get; set; }
	}
}
