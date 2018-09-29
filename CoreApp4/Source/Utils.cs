using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YllariFM.Source
{
    public class Utils
    {
        public static bool CiudadEsProvincia(string ciudad)
        {
            if (ciudad.ToLower() == "lima")
                return true;
            return false;
        }

        public static DateTime strinFechagADatetime(string horaFecha)
        {
            int anho = Convert.ToInt32(horaFecha.Split('-')[0]);
            int mes = Convert.ToInt32(horaFecha.Split('-')[1]);
            int dia  = Convert.ToInt32(horaFecha.Split('-')[2]);
            return new DateTime(anho,mes,dia);
        }

        public static TimeSpan stringHoraATime(string hora)
        {
            return new DateTime(2000, 1, 1, Convert.ToInt32(hora.Split(':')[0]), Convert.ToInt32(hora.Split(':')[1]), 0).TimeOfDay;
        }


        public static string GetFullMensajeExcepcion(Exception ex)
        {
            string msj = ex.Message;
            if (ex.InnerException == null)
                return msj;
            else
                return msj + " > " + GetFullMensajeExcepcion(ex.InnerException);
        }

    }
}
