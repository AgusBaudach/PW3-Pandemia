using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AyudandoEnLaPandemia.Models;

namespace AyudandoEnLaPandemia.Servicios
{
    public class ServicioUsuarios
    {
       private static List<Usuario> lista = new List<Usuario>();

        public List<Usuario> listar()
        {
            return lista;
        }

        public void ingresarUsuario(Usuario usuario)
        {

            int Maxid = 0;
            if (lista.Count > 0)
            {
                Maxid = lista.Max(u => u.id);
            }

            usuario.id = Maxid + 1;
            lista.Add(usuario);
        }

        public Usuario buscarUsuario(int id)
        {

            foreach (Usuario u in lista)
            {
                if (u.id == id)
                {
                    return u;
                }

            }
            return null;
        }

        public Usuario buscarUsuario(string pass,string nombreUsuario)
        {

            foreach (Usuario u in lista)
            {
                if (u.Password == pass && u.NombreUsuario== nombreUsuario)
                {
                    return u;
                }

            }
            return null;
        }
    }
}