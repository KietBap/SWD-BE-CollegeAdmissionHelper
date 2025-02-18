using CAH.Core.Utils;
using Microsoft.AspNetCore.Identity;

namespace CAH.Contract.Repositories.Entity
{
	public class User : IdentityUser<Guid>
	{
		public string Password { get; set; } = string.Empty;
		public string? UserImage { get; set; }
		public string? CreatedBy { get; set; }
		public string? LastUpdatedBy { get; set; }
		public string? DeletedBy { get; set; }
		public DateTimeOffset CreatedTime { get; set; }
		public DateTimeOffset LastUpdatedTime { get; set; }
		public DateTimeOffset? DeletedTime { get; set; }
		public string? RefreshToken { get; set; }
		public DateTimeOffset RefreshTokenExpiryTime { get; set; }

		public User()
		{
			CreatedTime = CoreHelper.SystemTimeNow;
			LastUpdatedTime = CreatedTime;
		}
		public virtual ICollection<UserRole> UserRoles { get; set; }
		public virtual ICollection<Wishlist> Wishlists { get; set; }

	}
}
