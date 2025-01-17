﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YllariFM.Source
{
    public class Constantes
    {

        public class TipoServicio
        {
            public const string Transporte = "TRANS";
            public const string Servicio = "SERVI";
        }

        public class TipoProveedor {
            public const string Persona = "PERSO";
            public const string Empresa = "EMPRE";
        }

        public class TipoCliente {
            public const string Persona = "PERSO";
            public const string Empresa = "EMPRE";
            public static string aTexto(string tipo) {
                if (tipo == Persona)
                    return "Persona";
                else if (tipo == Empresa)
                    return "Empresa";
                else
                    throw new InvalidOperationException("TIPO INVALIDO");
            }
        }
    }
}
