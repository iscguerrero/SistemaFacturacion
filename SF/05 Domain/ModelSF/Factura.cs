using ModelSF.Commons;
using System;
using System.Collections.Generic;
namespace ModelSF {
	public class Factura : AuditEntity, ISoftDeleted {
		public int Folio { get; set; }
		public DateTime Fecha { get; set; }

		public bool IsDeleted { get; set; }

		public string RFCEmpresa { get; set; } // Empresa_RFC
		public virtual Empresa Empresa {get; set;}

		public string RFCCliente { get; set; } // Cliente_RFC
		public virtual Cliente Cliente {get; set;}

		public virtual ICollection<Partida> Partidas { get; set; }
	}
}
