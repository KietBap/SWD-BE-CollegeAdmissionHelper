using CAH.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAH.Contract.Repositories.Entity
{
	public class ComboAppointment : BaseEntity
	{
		public string ComboId { get; set; }

		[ForeignKey("ComboId")]
		public virtual Combo Combo { get; set; }

		public string AppointmentId { get; set; }

		[ForeignKey("AppointmentId")]
		public virtual Appointment Appointment { get; set; }
	}
}
