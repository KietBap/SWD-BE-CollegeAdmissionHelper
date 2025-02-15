using CAH.Core.Base;
using CAH.Repositories.Entity;

namespace CAH.Contract.Repositories.Entity
{
	public class UserInfo : BaseEntity
	{
		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string? BankAccount { get; set; }
		public virtual ApplicationUsers ApplicationUsers { get; set; }
		public string? BankAccountName { get; set; }
		public string? Bank { get; set; }

        public int Point { get; set; } = 0;
       
    }
}
