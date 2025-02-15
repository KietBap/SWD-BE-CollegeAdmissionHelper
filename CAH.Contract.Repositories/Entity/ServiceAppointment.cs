using CAH.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
	public class ServiceAppointment : BaseEntity
	{
		public string ServiceId { get; set; }

		[ForeignKey("ServiceId")]
		public virtual Service Service { get; set; }

		public string AppointmentId { get; set; }

		[ForeignKey("AppointmentId")]
		public virtual Appointment Appointment { get; set; }

		[MaxLength(255)]
		public string? Description { get; set; }

	}
}
