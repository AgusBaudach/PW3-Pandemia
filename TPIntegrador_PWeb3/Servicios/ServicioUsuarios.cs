using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using TPIntegrador_PWeb3.DAL;

namespace TPIntegrador_PWeb3.Servicios
{
    public class ServicioUsuarios: UsuarioRepositorio
    {

        public List<Usuarios> listar()
        {
            List<Usuarios> lista = ctx.Usuarios.ToList();
            return lista;
        }

        public void EnviarMail(Usuarios usuario)
        {
            String mail = usuario.Email;
            String mailFrom = "agustinabaudach7@gmail.com";
            var mailTo = new MailAddress(mail, "Token");
            var mailFromVar = new MailAddress(mailFrom, "De ayudandoEnLaPandemia");

            const string fromPassword = "ContraseñaMail";
            const string subject = "TOKEN";
            const string body = "Activar cuenta: https://TPIntegrador_PWeb3/Usuario/activar?token=324234242​";

            var smtp = new SmtpClient
            {

                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(mailFromVar.Address, fromPassword)
            };
            using (var message = new MailMessage(mailFrom, mail)
            {
                Subject = subject,
                Body = body
            })
            {
                // smtp.Send(message);
            }
        }


        public void ingresarUsuario(Usuarios usuario)
        {
            
           this.Crear(usuario);



        }

        public Usuarios traerUsuario(String password)
        {

            List<Usuarios> lista = ctx.Usuarios.ToList();
            Usuarios usuario = new Usuarios();

            foreach (Usuarios u in lista)
            {
                if (u.Password == password)
                {
                    usuario = u;
                }

            }

            return usuario;
        }

        public void ModificarUsuario(Usuarios usuario)
        {

            UsuarioRepositorio ur = new UsuarioRepositorio();
            ur.Modificar(usuario);




        }

       



        public Usuarios buscarUsuario(int id)
        {
            List<Usuarios> lista = ctx.Usuarios.ToList();
            foreach (Usuarios u in lista)
            {
                if (u.IdUsuario == id)
                {
                    return u;
                }

            }
            return null;
        }

        public Usuarios buscarUsuario(string pass, string email)
        {
            List<Usuarios> lista = ctx.Usuarios.ToList();
            Usuarios usuario = new Usuarios();

            foreach (Usuarios u in lista)
            {
                if (u.Password == pass && u.Email == email)
                {
                    usuario = u;
                }

            }
            return usuario;
        }
    }
}
