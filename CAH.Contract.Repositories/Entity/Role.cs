using CAH.Core.Utils;
using CAH.Contract.Repositories.Entity;
using Microsoft.AspNetCore.Identity;

namespace CAH.Contract.Repositories.Entity
{
    public class Role : IdentityRole<Guid>
    {
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }

    }
}