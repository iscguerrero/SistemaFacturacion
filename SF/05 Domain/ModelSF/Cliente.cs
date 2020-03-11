using ModelSF.Commons;
using System.Collections.Generic;
namespace ModelSF {
	public class Cliente : AuditEntity, ISoftDeleted {
		public int Id { get; set; }
		public string RFC { get; set; }
		public string Nombre { get; set; }

		public bool IsDeleted { get; set; }

		public virtual ICollection<Factura> Facturas { get; set; }
	}
}
