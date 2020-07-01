using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Historial.DAL
{
    public abstract class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        public static Entities1 ctx = new Entities1();

        public DbSet<T> dbSet = ctx.Set<T>();

        public void Crear(T entity)
        {
            dbSet.Add(entity);

            ctx.SaveChanges();
        }

        public void Eliminar(T entity)
        {
            throw new NotImplementedException();
        }

        public void Modificar(T entity)
        {
           // dbSet.AddOrUpdate(entity);
            ctx.SaveChanges();
        }

        public T ObtenerPorId(int id)
        {
            return dbSet.Find(id);
        }

        public List<T> ObtenerTodos()
        {
            return dbSet.ToList();
        }


    }
}