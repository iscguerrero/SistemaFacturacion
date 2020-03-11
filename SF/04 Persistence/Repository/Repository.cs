using ModelSF;
using ModelSF.Commons;
using PersistenceSF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
namespace RepositorySF {

	public class Repository<T> where T : class {
		protected SFContext ctx;

		#region Lectura
		public IQueryable<T> Set() {
			try {
				return ctx.Set<T>();
			} catch (Exception e) {
				return null;
			}
		}
		#endregion

		#region Create
		public T Add(T New) {
			try {
				var Added = ctx.Set<T>().Add(New);
				Bitacora(Added);
				return Added;
			} catch (Exception e) {
				return null;
			}
		}

		public IEnumerable<T> AddRange(IEnumerable<T> tNew) {
			try {
				var tAdded = ctx.Set<T>().AddRange(tNew);

				foreach (var t in tAdded)
					Bitacora(t);

				return tAdded;
			} catch (Exception e) {
				return null;
			}
		}
		#endregion

		#region Update
		public T NoTrackingUpdate(T edited, string key) {
			try {
				if (edited == null)
					return null;

				var existing = ctx.Set<T>().Find(key);
				if (existing == null)
					return null;

				ctx.Entry(existing).CurrentValues.SetValues(edited);
				Bitacora(existing);
				return existing;
			} catch (Exception e) {
				return null;
			}
		}

		public bool TrackingUpdate(T edited) {
			try {
				ctx.Entry(edited).State = EntityState.Modified;
				Bitacora(edited);
				return true;
			} catch (Exception e) {
				return false;
			}
		}
		#endregion

		#region Remove
		public T SoftDelete(string key) {
			try {
				var existing = ctx.Set<T>().Find(key);

				if (existing == null)
					return null;

				((ISoftDeleted)existing).IsDeleted = true;
				ctx.Set<T>().Attach(existing);
				ctx.Entry(existing).State = EntityState.Modified;
				Bitacora(existing);
				return existing;
			} catch (Exception e) {
				return null;
			}
		}
		public T CompositePKSoftDelete(params object[] keys) {
			try {
				var existing = ctx.Set<T>().Find(keys);

				if (existing == null)
					return null;

				((ISoftDeleted)existing).IsDeleted = true;
				ctx.Set<T>().Attach(existing);
				ctx.Entry(existing).State = EntityState.Modified;
				Bitacora(existing);
				return existing;
			} catch (Exception e) {
				return null;
			}
		}

		// Hard Delete
		public bool Delete(T t) {
			try {
				ctx.Set<T>().Remove(t);
				Bitacora(t);
				return true;
			} catch (Exception e) {
				return false;
			}
		}

		public bool DeleteRange(List<T> tList) {
			try {
				ctx.Set<T>().RemoveRange(tList);

				foreach (var t in tList)
					Bitacora(t);

				return true;
			} catch (Exception e) {
				return false;
			}
		}
		#endregion

		#region Bitacora
		protected void Bitacora(T t) {
			var entry = ctx.Entry(t);

			if (entry.Entity != null) {
				string correo = "siga@usebeq.edu.mx";

				string[] PKNames = PKNames<T>.Get(ctx);

				string PKName = string.Join("|", PKNames);

				List<string> PKValues = new List<string>();
				foreach (string item in PKNames) {
					PKValues.Add(entry.State.ToString() == "Added" ? "0" : entry.OriginalValues[item].ToString());
				}
				string PKValue = string.Join("|", PKValues);

				// Obtenemos el nombre de las propiedades de la entidad
				var properties = entry.State.ToString() == "Deleted" ? entry.OriginalValues.PropertyNames : entry.CurrentValues.PropertyNames;

				foreach (var propName in properties) {

					var original = entry.State.ToString() == "Added" ? "" : entry.OriginalValues[propName];
					var current = entry.State.ToString() == "Deleted" ? "" : entry.CurrentValues[propName];

					if (propName == "Deleted" && entry.State.ToString() == "Modified")
						original = "False";

					if (Convert.ToString(current) != Convert.ToString(original)) {
						ctx.Bitacora.Add(new Bitacora {
							BDOrigen = ctx.Database.Connection.Database,
							EntidadOrigen = entry.Entity.GetType().Name,
							Accion = entry.State.ToString(),
							Propiedad = propName,
							PKValue = PKValue,
							PKName = PKName,
							TipoDato = Convert.ToString(current.GetType()),
							ValorAntes = Convert.ToString(original),
							ValorDespues = Convert.ToString(current),
							Fecha = DateTime.Now,
							Correo = correo
						});
					}

				}

			}

		}
		#endregion
	}


	public static class PKNames<T> where T : class {

		public static string[] Get(SFContext ctx) {
			ObjectContext objectContext = ((IObjectContextAdapter)ctx).ObjectContext;

			ObjectSet<T> set = objectContext.CreateObjectSet<T>();

			string[] PKNames = set.EntitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();

			return PKNames;
		}
	}

}
