using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;

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

        public static string MesIntATexto(int mes)
        {
            if (mes == 1)
                return "Enero";
            if (mes == 2)
                return "Febrero";
            if (mes == 3)
                return "Marzo";
            if (mes == 4)
                return "Abril";
            if (mes == 5)
                return "Mayo";
            if (mes == 6)
                return "Junio";
            if (mes == 7)
                return "Julio";
            if (mes == 8)
                return "Agosto";
            if (mes == 9)
                return "Septiembre";
            if (mes == 10)
                return "Octubre";
            if (mes == 11)
                return "Noviembre";
            if (mes == 12)
                return "Diciembre";
            else
                throw new InvalidOperationException("MES " + mes + " NO ES TRADUCIBLE A TEXTO.");

        }

        public static string getNombreBiblia(Biblia biblia)
        {
            return MesIntATexto(biblia.Mes) + " " + biblia.Anho;
        }

        public static DateTime fechaPeruanaActual()
        {
            return DateTime.UtcNow.Subtract(new TimeSpan(5, 0, 0));
        }
    }
}
