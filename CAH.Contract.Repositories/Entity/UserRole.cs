using CAH.Core.Utils;
using Microsoft.AspNetCore.Identity;


namespace CAH.Contract.Repositories.Entity
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }

        public UserRole()
        {
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }
    }
}
