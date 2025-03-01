using CAH.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
    public class SubjectScore : BaseEntity
    {
        [Required]
        public string SubjectId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public DateOnly Year { get; set; } 

        public float Score { get; set; }
        public string ExamType { get; set; }
        public string? Class { get; set; }
    }
}
