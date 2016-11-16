using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace Datos
{
    public class Consultas
    {

//maycol



        public DataTable Select(string comando)
        {
            return MetodosDatos.EjecutarConsula(comando);
        }

        public DataTable Select(string columnas, string tabla)
        {
            string comando = "SELECT " + columnas + " FROM " + tabla;
            return MetodosDatos.EjecutarConsula(comando);
        }

        public DataTable Select(string columnas, string tabla, string where)
        {
            where = where.Replace("''", "null");
            string comando = "SELECT " + columnas + " FROM " + tabla + " WHERE " + where;
            return MetodosDatos.EjecutarConsula(comando);
        }

        public DataTable Select_inner_join(string cadena1, string tabla_uno, string tabla_dos, string parametro)
        {

            string comando = "SELECT " + cadena1 + " FROM " + tabla_uno + " INNER JOIN  " + tabla_dos + "  ON(" + parametro + ")";
            return MetodosDatos.EjecutarConsula(comando);

        }

        public DataTable Select_secuencia(string secuencia, string AS_nombre)
        {

            string comando = "SELECT currval('" + secuencia + "') AS " + AS_nombre;
            return MetodosDatos.EjecutarConsula(comando);

        }

        public NpgsqlDataAdapter Select_reporte(string cadena1, string tabla, string parametro)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(MetodosDatos.cadenaConexion);
            conexion.Open();

            NpgsqlDataAdapter consulta = new NpgsqlDataAdapter("SELECT " + cadena1 + " FROM " + tabla + " WHERE " + parametro + "  ", conexion);

            conexion.Close();

            return consulta;
        }

        public int Insert(string datos, string columnas, string tabla)
        {
            datos = datos.Replace("''", "null");
            NpgsqlConnection conexion = new NpgsqlConnection(MetodosDatos.cadenaConexion);
            NpgsqlCommand comando = new NpgsqlCommand("insert into " + tabla + " (" + columnas + ") values (" + datos + ")", conexion);
            return MetodosDatos.EjecutarComando(comando);
        }


        public int Insert(string datos, string columnas, string tipoDatos, string funcion)
        {
            datos = datos.Replace("''", "null");
            NpgsqlCommand comando = MetodosDatos.CrearComandoProc(funcion);
            if (datos.Contains("?"))
            {
                string[] vector1 = Vector(datos);
                string[] vector2 = Vector(columnas);
                string[] vector3 = Vector(tipoDatos);

                for (int i = 0; i < vector1.Length; i++)
                {
                    comando.Parameters.Add(new NpgsqlParameter(vector2[i], vector3[i]));
                    comando.Parameters[i].Value = vector1[i];
                }
            }
            else
            {
                comando.Parameters.Add(new NpgsqlParameter(columnas, tipoDatos));
                comando.Parameters[0].Value = datos;
            }

            return MetodosDatos.EjecutarComando(comando);
        }

        public int Delete(string where, string tabla)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(MetodosDatos.cadenaConexion);
            NpgsqlCommand comando = new NpgsqlCommand("DELETE from " + tabla + " where " + where, conexion);
            return MetodosDatos.EjecutarComando(comando);
        }

        public int Update(string tabla, string campo, string where)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(MetodosDatos.cadenaConexion);
            NpgsqlCommand _comando = new NpgsqlCommand("UPDATE  " + tabla + " SET " + campo + " WHERE " + where, conexion);

            return MetodosDatos.EjecutarComando(_comando);
        }


        public static string[] Vector(string cadena)
        {
            string[] vector;

            if (cadena.Contains('?'))
            {
                vector = cadena.Split('?');
            }
            else
            {
                vector = new string[1];
                vector[0] = cadena;
            }
            return vector;
        }




    }
}
