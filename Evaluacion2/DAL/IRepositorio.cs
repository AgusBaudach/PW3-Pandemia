using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion2.DAL
{
    interface IRepositorio<T>
    {
        T ObtenerPorId(int? id);
        List<T> ObtenerTodos();
        void Crear(T entity);
        void Modificar(T entity);
        void Eliminar(T entity);
    }
}
