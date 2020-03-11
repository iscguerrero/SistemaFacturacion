using PersistenceSF;
using System;
namespace UoWSF {
	public interface IUoW {
		IUoWRepository Repository { get; }
		void SaveChanges();
		void Dispose();
	}

	public class UoWContainer : IUoW {
		private readonly SFContext _context;
		public IUoWRepository Repository { get; set; }

		public UoWContainer(SFContext context) {
			this._context = new SFContext();
			Repository = new UoWRepository(_context);
		}

		public void SaveChanges() {
			try {
				_context.SaveChanges();
			} catch (System.Data.Entity.Validation.DbEntityValidationException dbEx) {
				Exception raise = dbEx;
				foreach (var validationErrors in dbEx.EntityValidationErrors) {
					foreach (var validationError in validationErrors.ValidationErrors) {
						string message = string.Format("{0}:{1}",
								validationErrors.Entry.Entity.ToString(),
								validationError.ErrorMessage);
						raise = new InvalidOperationException(message, raise);
					}
				}
				throw raise;
			}
		}

		public void Dispose() {
			_context.Dispose();
		}
	}
}
