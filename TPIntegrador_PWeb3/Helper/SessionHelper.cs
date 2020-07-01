using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPIntegrador_PWeb3.Helper
{
  
        public class SessionHelper
        {
            public static string Nombre
            {
                get
                {
                    return HttpContext.Current.Session["Nombre"] as string;
                }
                set
                {
                    HttpContext.Current.Session["Nombre"] = value;
                    HttpContext.Current.Session.Timeout = 30;
                }
            }

        public static string Email
        {
            get
            {
                return HttpContext.Current.Session["Email"] as string;
            }
            set
            {
                HttpContext.Current.Session["Email"] = value;
               
            }
        }

        public static int? IdNecesidad
        {
            get
            {
                return HttpContext.Current.Session["IdNecesidad"] as int?;
            }
            set
            {
                HttpContext.Current.Session["IdNecesidad"] = value;

            }
        }

        public static DateTime FechaCreacionSession
            {
                get
                {
                    return (DateTime)HttpContext.Current.Session["FechaCreacionSession"];
                }
                set
                {
                    HttpContext.Current.Session["FechaCreacionSession"] = value;
                }
            }



        }
    
}