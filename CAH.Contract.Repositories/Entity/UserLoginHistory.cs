using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CAH.Contract.Repositories.Entity
{
    public class UserLoginHistory : IdentityUserLogin<Guid>
    {
        [Key] // ✅ Định nghĩa khóa chính
        public Guid Id { get; set; } = Guid.NewGuid(); // 🔹 Đổi sang Guid thay vì string

        public Guid UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; } // ✅ Đảm bảo không bị NULL
        public DateTimeOffset LoginTime { get; set; } = DateTimeOffset.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    }
}
