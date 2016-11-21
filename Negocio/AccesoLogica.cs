using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Datos;
using Npgsql;

namespace Negocio
{
    public class AccesoLogica
    {
        public static bool Autenticar(string usuario, string password)
        {
            Consultas fun = new Consultas();
            DataTable tabla = fun.Select("*", "usuarios", "usuario_usuario = '" + usuario + "'  AND clave_usuario = '" + password + "'");

            int registros = tabla.Rows.Count;
            if (registros > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataTable Select(string comando)
        {
            Consultas fun = new Consultas();
            return fun.Select(comando);
        }

        public static DataTable Select(string columnas, string tabla)
        {
            Consultas fun = new Consultas();
            return fun.Select(columnas, tabla);
        }

        public static DataTable Select(string columnas, string tabla, string where)
        {
            Consultas fun = new Consultas();
            return fun.Select(columnas, tabla, where);
        }

        public static DataTable Select(string columnas, string tabla, string where, string order)
        {
            Consultas fun = new Consultas();
            return fun.Select(columnas, tabla, where, order);
        }

        public static DataTable Select_inner_join(string cadena1, string tabla_uno, string tabla_dos, string parametro)
        {
            Consultas fun = new Consultas();
            return fun.Select_inner_join(cadena1, tabla_uno, tabla_dos, parametro);

        }
        public static DataTable Select_secuencia(string secuencia, string AS_nombre)
        {
            Consultas fun = new Consultas();
            return fun.Select_secuencia(secuencia, AS_nombre);

        }
        public static NpgsqlDataAdapter Select_reporte(string cadena1, string tabla, string parametro)
        {
            Consultas fun = new Consultas();
            return fun.Select_reporte(cadena1, tabla, parametro);

        }



        public static int Insert(string datos, string columnas, string tabla)
        {
            Consultas fun = new Consultas();
            return fun.Insert(datos, columnas, tabla);
        }

        public static int Insert(string datos, string columnas, string tipoDatos, string funcion)
        {
            Consultas fun = new Consultas();
            return fun.Insert(datos, columnas, tipoDatos, funcion);
        }

        public static int Delete(string where, string tabla)
        {
            Consultas fun = new Consultas();
            return fun.Delete(where, tabla);
        }

        public static int Update(string tabla, string campo, string where)
        {
            Consultas fun = new Consultas();
            return fun.Update(tabla, campo, where);
        }
    }
}