using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Evaluacion2.DAL
{
    public abstract class RepositorioBase<T>: IRepositorio<T> where T : class
    {
        public static Evaluacion2Entities ctx = new Evaluacion2Entities();
        public DbSet<T> dbSet = ctx.Set<T>();


        public void Crear(T entity)
        {


            dbSet.Add(entity);

            ctx.SaveChanges();


        }





        public void Eliminar(T entidadAEliminar)
        {
            
        }



        public T ObtenerPorId(int? id)
        {
            return dbSet.Find(id);
        }

        public List<T> ObtenerTodos()
        {
            return dbSet.ToList();
        }

        public void Modificar(T entity)
        {
            dbSet.AddOrUpdate(entity);

            ctx.SaveChanges();

        }

        
    }
}
   
