using System;
using System.Collections.Generic;
using System.Globalization;
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
        /// <summary>
        /// Formato 1992-10-28
        /// </summary>
        /// <param name="horaFecha"></param>
        /// <returns></returns>
        public static DateTime stringFechaADatetime(string horaFecha)
        {
            int anho = Convert.ToInt32(horaFecha.Split('-')[0]);
            int mes = Convert.ToInt32(horaFecha.Split('-')[1]);
            int dia  = Convert.ToInt32(horaFecha.Split('-')[2]);
            return new DateTime(anho,mes,dia);
        }


        public static string datetimeAString(DateTime fecha)
        {
            return fecha.ToString("yyyy-MM-dd");
        }

        public static TimeSpan stringHoraATime(string hora)
        {
            if (hora.Split(':').Length < 2)
                throw new Exception("Hora invalida");
            return new DateTime(2000, 1, 1, Convert.ToInt32(hora.Split(':')[0]), Convert.ToInt32(hora.Split(':')[1]), 0).TimeOfDay;
        }

        public static string timespanAStringHora(TimeSpan time){
            return time.Hours + ":" + time.Minutes;
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


        public static string formatoFecha( DateTime date)
        {
            string fecha = date.Day+" "+MesIntATexto(date.Month)+" "+date.Year+" "+date.Hour+":"+date.Minute;
            return fecha;
        }

        public static bool IsValidEmail(string email) {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch {
                return false;
            }
        }
        public static bool stringVacio(string s) {
            return !stringLleno(s);
        }

        public static bool stringLleno(string s) {
            return !(String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s));
        }

        public static string listaStringsAListaHtml(List<string> strings) {
            string ss = "";
            var modelErrors = new List<string>();
            int i = 0;
            foreach (var v in strings) {
                    i++;
                    if (i != 1) ;
                    ss += "</br>";
                    modelErrors.Add(v);
                    ss += i + ". " + v;
            }
            return ss;
        }

        public static string getHtmlStringErroresModelState(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ms) {
            string ss = "";
            var modelErrors = new List<string>();
            int i = 0;
            foreach (var modelState in ms.Values) {
                foreach (var modelError in modelState.Errors) {
                    i++;
                    if (i != 1) ;
                    ss += "</br>";
                    modelErrors.Add(modelError.ErrorMessage);
                    ss += i + ". " + modelError.ErrorMessage;
                }
            }
            return ss;
        }
    }
}
