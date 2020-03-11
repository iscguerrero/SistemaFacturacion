using ModelSF.Commons;
namespace ModelSF {
	public class Partida : AuditEntity, ISoftDeleted {
		public int Folio { get; set; }
		public int NumerPartida { get; set; }
		public float Precio { get; set; }
		public float Piezas { get; set; }
		public float IVA { get; set; }

		public bool IsDeleted { get; set; }

		public int FolioProducto { get; set; }
		public virtual Producto Producto { get; set; }

		public int FolioFactura { get; set; }
		public virtual Factura Factura { get; set; }
	}
}
