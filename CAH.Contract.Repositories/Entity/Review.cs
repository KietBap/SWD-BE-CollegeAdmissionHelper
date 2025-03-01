using CAH.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
    public class Review : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public string UniMajorId { get; set; }

        [ForeignKey("UniMajorId")]
        public virtual UniMajor UniMajor { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
