using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPIntegrador_PWeb3.DAL;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Migrations;

namespace TPIntegrador_PWeb3.DAL
{
    public abstract class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        public static Entities5 ctx = new Entities5();

        public DbSet<T> dbSet = ctx.Set<T>();

        public void Crear(T entity)
        {
            dbSet.Add(entity);
            
            ctx.SaveChanges();
        }

        public void Eliminar(T entity)
        {
            dbSet.Remove(entity);

            ctx.SaveChanges();

        }

        public void Modificar(T entity)
        {
            dbSet.AddOrUpdate(entity);
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