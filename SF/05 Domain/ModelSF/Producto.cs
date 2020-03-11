using ModelSF.Commons;
using System.Collections.Generic;
namespace ModelSF {
	public class Producto: AuditEntity, ISoftDeleted {
		public int Folio { get; set; }
		public string Codigo { get; set; }
		public string Descripcion { get; set; }
		public float Costo { get; set; }
		public float Precio { get; set; }
		public bool GravaIVA { get; set; }
		public float TasaIVA { get; set; }

		public bool IsDeleted { get; set; }

		public virtual ICollection<Partida> Partidas { get; set; }
	}
}
