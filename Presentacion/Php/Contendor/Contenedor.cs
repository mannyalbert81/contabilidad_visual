using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Presentacion
{
    public class Contenedor
    {
        public DataTable Reporte()
        {

            DataTable dt_Reporte = new DataTable();

            string columnas = "plan_cuentas.nombre_plan_cuentas,entidades.nombre_entidades,plan_cuentas.nivel_plan_cuentas," +
                              "plan_cuentas.t_plan_cuentas,plan_cuentas.n_plan_cuentas,plan_cuentas.codigo_plan_cuentas";

            string tablas = "public.plan_cuentas, public.entidades";

            string where = "entidades.id_entidades = plan_cuentas.id_entidades AND entidades.id_entidades=3";

            dt_Reporte = AccesoLogica.Select(columnas, tablas, where);

            return dt_Reporte;
        }

        public DataSet PasaReporte()
        {
            DataTable dt_Reporte = new DataTable();
            DataSet ds_Reporte = new DataSet();

            string columnas = "plan_cuentas.nombre_plan_cuentas,entidades.nombre_entidades,plan_cuentas.nivel_plan_cuentas," +
                              "plan_cuentas.t_plan_cuentas,plan_cuentas.n_plan_cuentas,plan_cuentas.codigo_plan_cuentas";

            string tablas = "public.plan_cuentas, public.entidades";

            string where = "entidades.id_entidades = plan_cuentas.id_entidades AND entidades.id_entidades=3";

            dt_Reporte = AccesoLogica.Select(columnas, tablas, where);

            ds_Reporte.Tables.Add(dt_Reporte);

            return ds_Reporte;
        }

        public DataTable consulta()
        {

            DataTable dt = new DataTable();

            string columnas = "plan_cuentas.nombre_plan_cuentas,entidades.nombre_entidades,plan_cuentas.nivel_plan_cuentas," +
                           "plan_cuentas.t_plan_cuentas,plan_cuentas.n_plan_cuentas,plan_cuentas.codigo_plan_cuentas";

            string tablas = "public.plan_cuentas, public.entidades";

            string where = "entidades.id_entidades = plan_cuentas.id_entidades AND entidades.id_entidades=3";

            dt = AccesoLogica.Select(columnas, tablas, where);


            /*
            string txtNombre = row["nombre"].ToString();

            string txtApellido = row["apellido"].ToString();
            */

            return dt;
        }
    }
}